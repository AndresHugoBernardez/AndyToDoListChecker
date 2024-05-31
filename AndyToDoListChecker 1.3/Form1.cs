/*
 
 
AS: ♦Andy's Software♦  

____________________
|Andy S            |
|  ♦               |
|        ^         |
|       / \        |
|      /   \       |
|      \   /       |
|       \ /        |
|        v         |
|              ♦   |
|            Andy S|
|__________________|

presents:


Andy ToDo List Checker versión 1.3
==================================

2024 Lite version


Hecho por Andrés Hugo Bernárdez

1 de febrero del 2024 
 
Contacto: andreshugobernardez@gmail.com
 
New Features:
=============
v1.1 14 de enero 2024
Sub Items.

v1.2 1 de febrero 2024
Selector (rojo)
Copiar, Cortar y Pegar

v1.3 31 de mayo 2024
Se corrigieron varios Bugs
Se agrego arrastrar items y arrastrar pestañas
 


TODO LIST:





*/








using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AndyToDoListChecker
{





    public partial class Form1 : Form
    {
        const Boolean debugMode = false;
        const string versionATDLC = "1.3";
        const string fechaVersion = "31 de mayo del 2024";
        const string AboutActualizacion="Corrección de Bugs \r\n Capacidad de arrastrar elementos y pestañas \r\n";

        /// <summary>
        /// Aún sin usar.
        /// </summary>
        public class Arrastres {

            public int Activado { set; get; }

            public int PosicionInicial { set; get; }
            public int PosicionFinal { set; get; }

            public int Pintura { set; get; }
            public Arrastres()
            {
                Activado = 0;
                PosicionFinal = 0;
                PosicionInicial = 0;
                Pintura = 0;
            }

        }

        Arrastres Arrastre = new Arrastres();




        /// <summary>
        /// Un nodo es un item. Cada Item tiene un checkBox y un TextBox
        /// </summary>
        public class Nodo {

            public bool Estado { get; set; }


            public int Posicion { get; set; }

            public int HashTexto { get; set; }
            public int HashObjeto { get; set; }

            public Panel Objeto { get; set; }

            public System.Windows.Forms.TextBox Texto { get; set; }

            public System.Windows.Forms.CheckBox Checkin { get; set; }

            public int Tabs { get; set; }




            public Nodo()
            {
                Estado = false;

            }

            public Nodo(bool estado, string item)
            {
                Estado = estado;


                Tabs = 0;

            }
            public Nodo(bool estado, int hashobjeto, int hashtexto, int posicion, Panel objeto, System.Windows.Forms.TextBox texto, System.Windows.Forms.CheckBox checkin)
            {
                Estado = estado;

                HashTexto = hashtexto;

                Posicion = posicion;
                HashObjeto = hashobjeto;

                Tabs = 0;
                Objeto = objeto;
                Texto = texto;
                Checkin = checkin;





            }
        };


        /// <summary>
        /// ArchivoNodo coincide con cada pestaña y representa cada archivo abierto actualmente
        /// Contiene en Lista los items (Nodos)
        /// </summary>
        public class ArchivoNodo
        {
            public string Direccion { get; set; }
            public string Nombre { get; set; }
            public int Guardado { get; set; }

            public int Status { get; set; }

            public TabPage Pestaña { get; set; }


            public List<Nodo> Lista { get; set; }
            public int SelectedIndex { get; set; }


            public ArchivoNodo(string direccion, string nombre, int guardado, int indice)
            {
                Direccion = direccion;
                Nombre = nombre;
                Guardado = guardado;
                Status = indice;
                SelectedIndex = -1;

            }
        }


        /// <summary>
        /// item que se copia: Actualmente solo puede copiar un item (seleccionado en rojo) y no trabaja con portapapeles
        /// Solo funciona dentro del programa. 
        /// Proximamente: Se podrán copiar varios items
        /// Proximamente: El modo de copiar cambiará al modo texto usando #B# #1# contenido1 #P# #E#
        /// </summary>
        public class NodoCopia
        {
            public bool Estado { get; set; }
            public string Texto { get; set; }
            public int Tab { get; set; }

            public NodoCopia()
            {
                this.Estado = false;
                this.Texto = "";
                this.Tab = 0;
            }
            public NodoCopia(bool estado, string txt, int tab)
            {
                this.Estado = estado;
                this.Texto = txt;
                this.Tab = tab;
            }
            public void Settear(bool estado, string txt, int tab)
            {
                this.Estado = estado;
                this.Texto = txt;
                this.Tab = tab;
            }

        };

        /// <summary>
        /// Único nodo de copiar actualmente
        /// </summary>
        NodoCopia Copia1 = new NodoCopia();



        string direccionVentana = "none";
        Boolean ventanaGuardada = true;
        const string programaNombre = "Andy ToDo List Checker";
        string nombreVentana = "";




        /// <summary>
        /// Es la lista de archivos, única lista por programa abierto
        /// </summary>
        List<ArchivoNodo> Archivos = new List<ArchivoNodo>();


        //--------------------------------------------------------------

        public Form1()
        {

            InitializeComponent();

            this.MinimumSize = new System.Drawing.Size(400, 400);







            ResizeBegin += Form1_ResizeBegin;
            ResizeEnd += Form1_ResizeEnd;
            SizeChanged += Form1_Resize;
            FormClosing += Form1_FormClosing;


            /*this.rectangulo.MouseMove += Form1_MouseMove;
            this.rectangulo.MouseUp += Form1_MouseUp;
            this.rectangulo.MouseDown += Item_MouseDown;*/




        }

        /// <summary>
        /// Cuando cierra debe preguntar si se desean guardar cambios cuando corresponda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;


            if (Archivos.Count > 0)
            {
                PreguntarYCerrarVentana();

            }
            if (Archivos.Count == 0) e.Cancel = false;





        }

        const int BordeDerechoItem = 182;


        /// <summary>
        /// Cuando el programa (su ventana) cambia su tamaño,  usando el mouse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {

            int i, j, ni, nj;
            Control control = (Control)sender;




            Pestañas.Width = (control.Size.Width) - 130 - 20;
            Pestañas.Height = control.Size.Height - 65;
            panel2.Left = control.Size.Width - 135;
            panel2.Height = control.Size.Height - 65;

            //  paneles.Width = (control.Size.Width) - 130;
            //  paneles.Height = control.Size.Height - 65;


            ni = Archivos.Count;

            for (i = 0; i < ni; i++)
            {
                nj = Archivos[i].Lista.Count;
                for (j = 0; j < nj; j++)
                {

                    Archivos[i].Lista[j].Objeto.Width = control.Size.Width - BordeDerechoItem - 3;
                    Archivos[i].Lista[j].Texto.Width = control.Size.Width - BordeDerechoItem - 35;


                }
            }

            this.ResumeLayout();
        }
 
        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            this.SuspendLayout();
        }


        int maxi = 0;

        /// <summary>
        /// Cuando el programa (su ventana) cambia su tamaño, hay que tener en cuenta cuando se maximiza/minimiza por botón
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, System.EventArgs e)
        {


            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized && maxi == 0)
            {
                maxi = 1;
                Form1_SizeChanged(sender, e);

            }
            else if (this.WindowState == System.Windows.Forms.FormWindowState.Normal && maxi == 1)
            {
                maxi = 0;
                Form1_SizeChanged(sender, e);
            }

        }

        /// <summary>
        /// Cuando el programa (su ventana) cambia su tamaño, hay que tener en cuenta cuando se maximiza/minimiza por botón o se lo hace con el mouse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Form1_SizeChanged(object sender, EventArgs e)
        {
            int i, j, ni, nj;
            Control control = (Control)sender;


            this.Pestañas.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();

            Pestañas.Width = (control.Size.Width) - 140 - 10;
            Pestañas.Height = control.Size.Height - 65;
            panel2.Left = control.Size.Width - 135;
            panel2.Height = control.Size.Height - 65;
            //  paneles.Width = (control.Size.Width) - 130;
            //  paneles.Height = control.Size.Height - 65;


            ni = Archivos.Count;

            for (i = 0; i < ni; i++)
            {
                nj = Archivos[i].Lista.Count;
                for (j = 0; j < nj; j++)
                {

                    Archivos[i].Lista[j].Objeto.Width = control.Size.Width - BordeDerechoItem - 3;
                    Archivos[i].Lista[j].Texto.Width = control.Size.Width - BordeDerechoItem - 35;


                }
            }
            this.Pestañas.ResumeLayout();
            this.panel2.ResumeLayout();
            this.ResumeLayout();
        }

        private void Form1_Load(object sender, EventArgs e)
        {



        }

        //----------------------------------------------------------------



        /// <summary>
        /// Ante un cambio que afecte a la ventana general debe indicar si no se ha guardado para el 
        /// momento de cerrar el programa. 
        /// </summary>
        private void VentanaEstaGuardada()
        {


            if (ventanaGuardada)
            {
                guardarToolStripMenuItem1.Enabled = false;
                //guardarToolStripMenuItem1.PerformLayout();
            }
            else
            {
                guardarToolStripMenuItem1.Enabled = true;
                //guardarToolStripMenuItem1.PerformLayout();
            }
        }


        /// <summary>
        /// Nuevo Archivo, se añade a la lista Archivos del programa
        /// </summary>
        /// <param name="Direccion"></param>
        public void Nuevo_Archivo(String Direccion)
        {

            int i, posicion, j;

            Boolean seguir;


            i = Archivos.Count;

            seguir = false;
            j = 0;
            do
            {
                j++;
                seguir = true;
                foreach (ArchivoNodo archNodo in Archivos)
                {
                    if (("Nuevo" + j.ToString() + ".atd") == archNodo.Nombre) seguir = false;
                }

            }
            while (!seguir);

            if (Direccion == "" || Direccion == "none")
                Archivos.Add(new ArchivoNodo("none", "Nuevo" + j.ToString() + ".atd", 0, 1));
            else
            {
                posicion = Direccion.LastIndexOf('\\');
                Archivos.Add(new ArchivoNodo(Direccion, Direccion.Substring(posicion + 1), 1, 1));
            }










            Archivos[i].Pestaña = new System.Windows.Forms.TabPage();





            Archivos[i].Lista = new List<Nodo>();

            this.Pestañas.Controls.Add(Archivos[i].Pestaña);

            Archivos[i].Pestaña.AutoScroll = true;
            Archivos[i].Pestaña.Location = new System.Drawing.Point(4, 22);
            Archivos[i].Pestaña.Name = "tab" + (i + 1).ToString();
            Archivos[i].Pestaña.Padding = new System.Windows.Forms.Padding(3);
            Archivos[i].Pestaña.Size = new System.Drawing.Size(644, 359);
            Archivos[i].Pestaña.TabIndex = 0;
            Archivos[i].Pestaña.Text = Archivos[i].Nombre;
            Archivos[i].Pestaña.UseVisualStyleBackColor = true;










            this.Pestañas.SelectedIndex = i;




            // this.panel2.PerformLayout();




        }

  







        /// <summary>
        /// Retorna el primer número disponible desde 1 que no haya sido usado en la posicion de cada nodo delista Archivos[i].Lista
        /// </summary>
        /// <returns></returns>
        private int PosicionDisponible()
        {
            int i, j, k, n;
            Boolean PosicionExistente;

            i = Pestañas.SelectedIndex;
            n = Archivos[i].Lista.Count;
            j = 0;
            if (n > 0)
            {
                PosicionExistente = true;

                while (PosicionExistente)
                {
                    PosicionExistente = false;
                    j++;
                    for (k = 0; k < n && !PosicionExistente; k++)
                        if (j == Archivos[i].Lista[k].Posicion) PosicionExistente = true;

                }
                if (!PosicionExistente) return j;
                else return 0;//es un error que nunca debe suceder
            }
            else return 1;// si la lista no tiene elementos se empieza en 1.
        }

        //***********************TAMAÑO ITEMS*************************
        // Configuración de tamaños de las cajas
     
        Point punto = new Point(0, 0);
        const int CajaAlto = 45;
        const int posItem = 45;
        const int AltoBoton = 20;
        const int PosicionYBoton1 = 2;
        const int posicionYtxtchk = 5;
        const int AnchoBoton = 22;
        const int EspacioBoton = 3;
        const int ItemTabCero = 3;

        const int TabuladorIzquierdo = 30;



        /// <summary>
        /// Sirve para Agregar un item.
        /// </summary>
        /// <param name="chhhkIN"> Indica si el checkbox es true o false</param>
        /// <param name="txxxtIN"> Texti del item</param>
        /// <param name="tabulacion"> Que tabulación tiene (que corrimiento tiene)</param> 
        /// <param name="donde" >Si es 0 lo agrega debajo o en el último lugar, si es 1 lo agrega en el lugar de Archivos[Pestañas.SelectedIndex].SelectedIndex</param> 
        /// <returns></returns>
        public int AgregarBoton(Boolean chhhkIN, String txxxtIN, int tabulacion, int donde)
        {
            int auxiliar = 0;
            int i, NuevaPosicion, tabs, n, Seleccionado, hijo;


            Boolean chhhk;
            String txxxt;
            chhhk = chhhkIN;
            txxxt = txxxtIN;
            tabs = -1;


            if (Archivos.Count > 0)
            {

                i = this.Pestañas.SelectedIndex;
                Seleccionado = Archivos[i].SelectedIndex;

                n = Archivos[i].Lista.Count;
                if (n > 0 && Seleccionado > -1)
                {

                    if (donde == 1)
                    {
                        auxiliar = Seleccionado;
                    }
                    else
                    {
                        auxiliar = Seleccionado + 1;
                        tabs = Archivos[i].Lista[Seleccionado].Tabs;
                        // cuando el elemento tiene hijos, el nuevo elemento debe aparecer abajo de ellos
                        hijo = auxiliar;

                        while (hijo < n && Archivos[i].Lista[hijo].Tabs > tabs)
                        {
                            hijo++;
                        }
                        auxiliar = hijo;
                    }







                }
                else auxiliar = n;
                NuevaPosicion = PosicionDisponible();



                CheckBox chk = new System.Windows.Forms.CheckBox();
                System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();





                Panel item = new System.Windows.Forms.Panel();
                //item.SuspendLayout();

                // item1
                // 

                item.Controls.Add(txt);
                item.Controls.Add(chk);


                item.Location = new System.Drawing.Point(ItemTabCero - Archivos[i].Pestaña.HorizontalScroll.Value, 3 + auxiliar * posItem - Archivos[i].Pestaña.VerticalScroll.Value);
                item.Name = "item" + i.ToString() + "-" + auxiliar.ToString();
                item.Size = new System.Drawing.Size(643 + 90, CajaAlto);
                item.TabIndex = 4;
                item.TabStop = true;
                item.BackColor = SystemColors.Control;
               
                item.KeyUp += Item_KeyUp;
                item.Paint += Item_Paint;
                item.MouseDown += Item_MouseDown;


                item.MouseUp += Item_MouseUp;

                item.MouseMove += Item_MouseMove;






                // 
                // chk1
                // 
                chk.AutoSize = true;
                chk.Location = new System.Drawing.Point(7, posicionYtxtchk);
                chk.Name = "chk" + i.ToString() + "-" + auxiliar.ToString();
                chk.Size = new System.Drawing.Size(15, 14);
                chk.TabIndex = 1;
                chk.UseVisualStyleBackColor = true;
                chk.Checked = chhhk;
                chk.Click += CambioNodo;

                // 
                // txt1
                // 
                txt.Location = new System.Drawing.Point(25, posicionYtxtchk);
                txt.Multiline = true;
                txt.Name = "txt" + i.ToString() + "-" + auxiliar.ToString();
                txt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
                txt.Size = new System.Drawing.Size(605, 2 * AltoBoton - 7);
                txt.TabIndex = 0;
                txt.Text = txxxt;
                if (debugMode) txt.Text += "<" + auxiliar + ">";
                txt.TextChanged += CambioNodo;
                txt.GotFocus += Txt_GotFocus;
                //txt.LostFocus += AchicarTexto;
                //txt.GotFocus += AgrandarTexto;








                item.Width = this.ClientSize.Width - BordeDerechoItem + 16 - 3;
                txt.Width = this.ClientSize.Width - BordeDerechoItem + 16 - 35;


                //punto.X = 0;
                // punto.Y = this.tab1.VerticalScroll.Value;

                // this.tab1.AutoScrollPosition = punto ;

                Archivos[i].Pestaña.Controls.Add(item);



                if (auxiliar < n)
                {
                    Archivos[i].Lista.Insert(auxiliar, new Nodo(chhhk, item.GetHashCode(), txt.GetHashCode(), NuevaPosicion, item, txt, chk));
                    CorrerTodoDebajo(1, auxiliar);
                }
                else if (auxiliar == n)
                {
                    Archivos[i].Lista.Add(new Nodo(chhhk, item.GetHashCode(), txt.GetHashCode(), NuevaPosicion, item, txt, chk));
                }
                //txt.Focus();


                if (tabulacion > -1 && auxiliar > 0)
                {
                    Archivos[i].Lista[auxiliar].Tabs = tabulacion;
                    Archivos[i].Lista[auxiliar].Objeto.Left = 50 * tabulacion - Archivos[i].Pestaña.HorizontalScroll.Value + ItemTabCero;
                }
                else if (auxiliar == 0)
                {
                    Archivos[i].Lista[auxiliar].Tabs = 0;
                    Archivos[i].Lista[auxiliar].Objeto.Left = -Archivos[i].Pestaña.HorizontalScroll.Value + ItemTabCero;
                }

                else if (donde != 1)
                {

                    if (tabs > 0)
                    {

                        Archivos[i].Lista[auxiliar].Tabs = tabs;
                        Archivos[i].Lista[auxiliar].Objeto.Left = 50 * tabs - Archivos[i].Pestaña.HorizontalScroll.Value + ItemTabCero;

                    }
                }


                // item.ResumeLayout(false);
                // item.PerformLayout();


                //EnfocarItem(i, auxiliar, auxiliar + 1);





            }

            return (auxiliar);

        }


        /// <summary>
        /// Utilizado para el arrastre de Items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_MouseMove(object sender, MouseEventArgs e)
        {
            int index, n, item, j, posicionrelativa;
            if (Arrastre.Activado == 1)
            {


                index = Pestañas.SelectedIndex;
                n = Archivos[index].Lista.Count;

                item = Arrastre.PosicionInicial;
                posicionrelativa = (e.Location.Y - 3) / posItem;//-Archivos[index].Pestaña.VerticalScroll.Value)
                if (e.Location.Y < 0) posicionrelativa = posicionrelativa - 1;

                posicionrelativa = item + posicionrelativa;
                if (-1 < posicionrelativa && posicionrelativa < n && posicionrelativa != item)
                {
                    if (Archivos[index].Lista[posicionrelativa].Objeto.BackColor == SystemColors.Control)
                    {
                        Archivos[index].Lista[posicionrelativa].Objeto.BackColor = SystemColors.ControlDark;
                        Archivos[index].Lista[posicionrelativa].Objeto.Invalidate();

                        for (j = 0; j < n; j++)
                        {
                            if (j != posicionrelativa && Archivos[index].Lista[j].Objeto.BackColor == SystemColors.ControlDark)
                            {
                                Archivos[index].Lista[j].Objeto.BackColor = SystemColors.Control;
                                Archivos[index].Lista[j].Objeto.Invalidate();
                            }
                        }
                        Arrastre.Pintura = 1;
                    }
                } else
                {
                    if (Arrastre.Pintura == 1)
                    {
                        Arrastre.Pintura = 0;
                        for (j = 0; j < n; j++)
                        {
                            if (Archivos[index].Lista[j].Objeto.BackColor == SystemColors.ControlDark)
                            {
                                Archivos[index].Lista[j].Objeto.BackColor = SystemColors.Control;
                                Archivos[index].Lista[j].Objeto.Invalidate();
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Utilizado para el arrastre de items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_MouseUp(object sender, MouseEventArgs e)
        {
            int index, item, j, n, posicionrelativa;




            if (Arrastre.Activado == 1)
            {

                Arrastre.Activado = 0;
                Cursor = Cursors.Default;
                index = Pestañas.SelectedIndex;
                n = Archivos[index].Lista.Count;

                item = Arrastre.PosicionInicial;
                for (j = 0; j < n; j++)
                {
                    if (Archivos[index].Lista[j].Objeto.BackColor == SystemColors.ControlDark)
                    {
                        Archivos[index].Lista[j].Objeto.BackColor = SystemColors.Control;
                        Archivos[index].Lista[j].Objeto.Invalidate();
                    }
                }

                if (item > -1 && item < n)
                {
                    posicionrelativa = (e.Location.Y - 3) / posItem;//-Archivos[index].Pestaña.VerticalScroll.Value)
                    if (e.Location.Y < 0) posicionrelativa = posicionrelativa - 1;

                    posicionrelativa = item + posicionrelativa;


                    if (posicionrelativa > -1 && posicionrelativa < n)
                    {
                        Arrastre.PosicionFinal = posicionrelativa;

                        if (Arrastre.PosicionInicial != Arrastre.PosicionFinal)
                        {

                            Archivos[index].SelectedIndex = Arrastre.PosicionInicial;
                            CopiarItem();
                            BorrarItem();
                            //if (Arrastre.PosicionFinal > Arrastre.PosicionInicial) Arrastre.PosicionFinal--;
                            Archivos[index].SelectedIndex = Arrastre.PosicionFinal;

                            PegarItem();
                        }



                    }


                }

            }
        }




        /// <summary>
        /// Utilizado para el arrastre de items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_MouseDown(object sender, MouseEventArgs e)
        {
            int HashCode, i, n, index;

            HashCode = sender.GetHashCode();

            index = Pestañas.SelectedIndex;

            n = Archivos[index].Lista.Count;

            Archivos[index].SelectedIndex = -1;
            for (i = 0; i < n; i++)
            {


                if (Archivos[index].Lista[i].HashObjeto == HashCode || Archivos[index].Lista[i].HashTexto == HashCode)
                {
                    /*
                    // Create pen.
                    Pen blackPen = new Pen(Color.Black, 3);

                    // Create rectangle.
                    Rectangle rect = new Rectangle(0, 0, 200, 200);

                    // Draw rectangle to screen.


                    Archivos[index].Lista[i].Objeto.BorderStyle = BorderStyle.Fixed3D;
                    */

                    Archivos[index].Lista[i].Objeto.Focus();
                    Archivos[index].SelectedIndex = i;
                    Archivos[index].Lista[i].Objeto.Refresh();
                    Arrastre.PosicionInicial = i;
                }
                else /*if (Archivos[index].Lista[i].Objeto.BorderStyle == BorderStyle.Fixed3D)*/
                {
                    /* Archivos[index].Lista[i].Objeto.BorderStyle = BorderStyle.None;*/
                    Archivos[index].Lista[i].Objeto.Refresh();

                }
            }



            Arrastre.Activado = 1;
            Cursor = Cursors.NoMove2D;


        }



        //*******************Arrastre de pestañas********************
        // Variables globales para el arrastre de pestañas.
        // Mientras el mouse se mueve debe guardarse información sobre qué pestaña se mueve


        Point MouseIni;
        int MouseDragging = 0;
        
        int NewLeft = 0, NewRight = 0, CurrentLeft = 0, CurrentRight = 0;
        int CurrentPosition = 0;
        Point MouseFin;
        ArchivoNodo FileNode;

        /// <summary>
        /// Única función para el arrastre de pestañas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int  CurrentCount, k, CurrentTop, CurrentBottom,FinalPosition;
            if (e.Button == MouseButtons.Left)
            {



                if (MouseDragging == 0)
                {
                    CurrentCount = Pestañas.TabCount - 1;

                    // Guardamos posición del mouse con respecto al padre TabControl (sin tener en cuenta el scroll de pestañas)
                    MouseIni = e.Location;
                    // Activamos Mouse 
                    MouseDragging = 1;

                    Cursor = Cursors.NoMove2D;
                    NewLeft = 0;
                    NewRight = 0;


                    //label1.Text = "MIni:" + MouseIni.X.ToString() + ":" + MouseIni.Y.ToString();
                    CurrentPosition = Pestañas.SelectedIndex;
                    for (k = 0; k < Pestañas.TabCount; k++)
                    {
                        if (Pestañas.GetTabRect(k).X < MouseIni.X && MouseIni.X < Pestañas.GetTabRect(k).X + Pestañas.GetTabRect(k).Width)
                        {
                            CurrentPosition = k;
                            k = Pestañas.TabCount + 1;
                        }
                    }
                }



               if (MouseDragging == 1)
                {
                    MouseFin.X = e.X;
                    MouseFin.Y = e.Y;
                    //label1.Text = "MFin:" + MouseFin.X.ToString() + ":" + MouseFin.Y.ToString();



                  
                    
                    CurrentCount = Pestañas.TabCount - 1;
                    //FileNode = Archivos[CurrentPosition];

                    CurrentLeft = -NewLeft + Pestañas.GetTabRect(CurrentPosition).X;// -Corrimiento;// - Pestañas.AutoScrollOffset.X;
                    CurrentRight = NewRight + Pestañas.GetTabRect(CurrentPosition).X+Pestañas.GetTabRect(CurrentPosition).Width;// - Corrimiento;// - Pestañas.AutoScrollOffset.X;

                    /*
                    if (CurrentPosition > 0) 
                    {
                        if (Pestañas.GetTabRect(CurrentPosition - 1).Width > Pestañas.GetTabRect(CurrentPosition).Width) NewRight = (Pestañas.GetTabRect(CurrentPosition - 1).Width - Pestañas.GetTabRect(CurrentPosition).Width);
                        else NewRight = 0;
                    }

                    if(CurrentPosition <CurrentCount)
                    {
                        if (Pestañas.GetTabRect(CurrentPosition + 1).Width > Pestañas.GetTabRect(CurrentPosition).Width) NewLeft = (Pestañas.GetTabRect(CurrentPosition + 1).Width - Pestañas.GetTabRect(CurrentPosition).Width);
                        else NewLeft = 0;
                    }*/



                    CurrentTop = 0;
                    CurrentBottom = Pestañas.ItemSize.Height;

                    if (MouseFin.Y > CurrentTop && MouseFin.Y < CurrentBottom && MouseFin.X > Pestañas.Location.X && MouseFin.X < Pestañas.Size.Width+Pestañas.Location.X)
                    {
                        Cursor = Cursors.NoMove2D;
                        if (MouseFin.X < CurrentLeft && CurrentPosition > 0 )
                        {

                            //Detectar posicion final
                            FinalPosition = 0;
                            
                            for (k = 0; k < Pestañas.TabCount;k++)
                            {
                                if(Pestañas.GetTabRect(k).X<MouseFin.X && MouseFin.X < Pestañas.GetTabRect(k).X+Pestañas.GetTabRect(k).Width)
                                {
                                    FinalPosition = k;
                                    k = Pestañas.TabCount + 1;
                                }
                            }




                            if (Pestañas.GetTabRect(FinalPosition).Width > Pestañas.GetTabRect(CurrentPosition).Width)
                            {
                                NewLeft = 0;
                                NewRight = (Pestañas.GetTabRect(FinalPosition).Width - Pestañas.GetTabRect(CurrentPosition).Width);

                            }
                            else
                            {
                                NewRight = 0;
                                NewLeft = 0;
                            }


                            Pestañas.SuspendLayout();

                            Pestañas.SelectedIndex = -1;

                            FileNode = Archivos[CurrentPosition];

                            Pestañas.Controls.Remove(Archivos[CurrentPosition].Pestaña);


                            for (k = 0; k < FileNode.Lista.Count; k++)
                            {

                                FileNode.Pestaña.Controls.Add(FileNode.Lista[k].Objeto);
                            }
                            Archivos.Insert(FinalPosition, FileNode);
                            Pestañas.TabPages.Insert(FinalPosition, Archivos[FinalPosition].Pestaña);

                            Pestañas.SelectedIndex = FinalPosition;
                            


                            Archivos.RemoveAt(CurrentPosition + 1);
                            CurrentPosition = FinalPosition;
                            Pestañas.ResumeLayout();

                            //MouseDragging = 0;
                            Cursor = Cursors.Default;
                            ventanaGuardada = false;

                        }
                        else if (MouseFin.X > CurrentRight && CurrentPosition < CurrentCount)
                        {


                            //Detectar posicion final
                            FinalPosition = CurrentCount;

                            for (k = CurrentPosition; k < Pestañas.TabCount; k++)
                            {
                                if (Pestañas.GetTabRect(k).X < MouseFin.X && MouseFin.X < Pestañas.GetTabRect(k).X + Pestañas.GetTabRect(k).Width)
                                {
                                    FinalPosition = k;
                                    k = Pestañas.TabCount + 1;
                                }
                            }

                            if (Pestañas.GetTabRect(FinalPosition).Width > Pestañas.GetTabRect(CurrentPosition).Width)
                            {

                                NewLeft = (Pestañas.GetTabRect(FinalPosition).Width - Pestañas.GetTabRect(CurrentPosition).Width);
                                NewRight = 0;
                            }
                            else
                            {
                                NewLeft = 0; NewRight = 0;
                            }


                            Pestañas.SuspendLayout();

                            Pestañas.SelectedIndex = -1;

                            FileNode = Archivos[CurrentPosition];

                            Pestañas.Controls.Remove(Archivos[CurrentPosition].Pestaña);

                            for (k = 0; k < FileNode.Lista.Count; k++) 
                            {
                                FileNode.Pestaña.Controls.Add(FileNode.Lista[k].Objeto);
                            }
                            Archivos.Insert(FinalPosition+1, FileNode);
                            Pestañas.TabPages.Insert(FinalPosition, Archivos[FinalPosition+1].Pestaña);


                            Pestañas.SelectedIndex = FinalPosition;

                           



                            Archivos.RemoveAt(CurrentPosition);
                            CurrentPosition = FinalPosition;
                            Pestañas.ResumeLayout();

                            //MouseDragging = 0;
                            Cursor = Cursors.Default;
                            ventanaGuardada = false;

                        }

                    }
                    else
                    {
                        Cursor = Cursors.No;
                     
                           
                        
                    }




                }




            }
            else
            {
                if (MouseDragging == 1)
                {
                    MouseDragging = 0;
                    Cursor = Cursors.Default;
                    NewLeft = 0;
                    NewRight = 0;
                }
              
            }
            







        }

     
/// <summary>
/// Como cada Control está sobre el padre, al seleccionar el TextBox, el padre (el item) también debe seleccionarse
/// Así esta función hace que el item padre se seleccione si se hace foco en un textbox
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void Txt_GotFocus(object sender, EventArgs e)
        {
            int i,n, j, index,hashcode;


            j = -1;
            index = Pestañas.SelectedIndex;

            hashcode = sender.GetHashCode();
            n = Archivos[index].Lista.Count;

            for (i = 0; i < n && j == -1; i++)
                if (Archivos[index].Lista[i].HashTexto == hashcode) { j = i; }

            if (Archivos[index].SelectedIndex!=j)
            if(j>-1)EnfocarItem(index, j, n);
        }







        /// <summary>
        /// Esta es una función interna de otra. Los parámetros solo son para optimizar tiempo. 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"> Debe ser la pestaña actual</param>
        /// <param name="n">n debe ser la cantidad de items en esa pestaña, se usa para optimizar velocidad de código</param>
        /// <returns></returns>
        private int MoverItemArriba(int i, int j, int n)
        {

            int l, numeroDeHijos, tabs, hayHermano, tabsL, HermanoEHijos, retorno;
            Boolean seguir;

            Nodo temporal;
            retorno = -1;

            if (i > 0)

            {
                tabs = Archivos[j].Lista[i].Tabs;

                //encontrar próximo hermano arriba
                hayHermano = 0;

                l = i - 1;
                HermanoEHijos = 1;
                do
                {
                    tabsL = Archivos[j].Lista[l].Tabs;
                    if (tabsL == tabs) hayHermano = 1;
                    else if (tabsL < tabs) hayHermano = -1;
                    else if (tabsL > tabs) HermanoEHijos++;
                    l--;
                } while (l > -1 && hayHermano == 0);

                if (hayHermano == 1)
                {

                    //Encontrar hijos
                    seguir = true;
                    numeroDeHijos = 0;
                    if (i + 1 < n)
                    {
                        for (l = i + 1; l < n && seguir; l++)
                        {
                            tabsL = Archivos[j].Lista[l].Tabs;
                            if (tabsL <= tabs) seguir = false;
                            else
                            {
                                numeroDeHijos++;
                            }

                        }
                    }

                    //Realizar saltos

                    //Paso 1 Presentación Gráfica.


                    Archivos[j].Pestaña.SuspendLayout();
                    this.SuspendLayout();

                    //Mover fisicamente al Hermano y sus hijos la cantidad de hijos propios +1



                    for (l = 0; l < HermanoEHijos; l++)

                    {
                        punto = Archivos[j].Lista[i - HermanoEHijos + l].Objeto.Location;
                        punto.Y += posItem * (numeroDeHijos + 1);
                        Archivos[j].Lista[i - HermanoEHijos + l].Objeto.Location = punto;
                    }


                    //Mover fisicamente al item y a sus hijos la cantidad de hijos del hermano +1


                    for (l = 0; l < numeroDeHijos + 1; l++)

                    {

                        punto = Archivos[j].Lista[i + l].Objeto.Location;
                        punto.Y -= posItem * HermanoEHijos;
                        Archivos[j].Lista[i + l].Objeto.Location = punto;


                    }





                    Archivos[j].Pestaña.ResumeLayout(false);
                    Archivos[j].Pestaña.PerformLayout();
                    this.ResumeLayout(false);

                    //Acomodar la Lista



                    for (l = 0; l < HermanoEHijos; l++)
                    {
                        temporal = Archivos[j].Lista[i - HermanoEHijos];
                        Archivos[j].Lista.RemoveAt(i - HermanoEHijos);
                        Archivos[j].Lista.Insert(i + numeroDeHijos, temporal);
                    }


                    HuboCambio();

                    retorno = i-HermanoEHijos;

                }

                
            }



            return retorno;


        }



        /// <summary>
        /// Mover Item Seleccionado(rojo) hacia arriba, bajando el de arriba por el actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Arriba_Click(object sender, EventArgs e)
        {
            int i, n, j,nuevaSeleccion;
            
           
            j = this.Pestañas.SelectedIndex;


            if (Archivos.Count > 0)
            {
                n = Archivos[j].Lista.Count;
                if (n > 1)
                {
                    i = Archivos[j].SelectedIndex;


                    if (i >= 0 && i < n)
                    {


                        nuevaSeleccion = MoverItemArriba(i, j, n);
                        if (nuevaSeleccion > -1) EnfocarItem(j, nuevaSeleccion, n);




                    }


                }
            }

        }



        /// <summary>
        /// Mover Item Seleccionado(rojo) hacia abajo, subiendo el de abajo por el actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Abajo_Click(object sender, EventArgs e)
        {
            int i, n, j, l, k,hijos, tabs,nuevaSeleccion;
            Boolean  seguir;
           

            j = this.Pestañas.SelectedIndex;

           
            k = 0;
            if (Archivos.Count > 0)
            {
                n = Archivos[j].Lista.Count;
                if (n > 1)
                {

                    i = Archivos[j].SelectedIndex;

                    if (i >= 0 && i < n)
                    {
                        //Buscar siguiente hermano

                        tabs = Archivos[j].Lista[i].Tabs;
                        seguir = true;

                        for (l = i + 1; l < n && seguir; l++)
                        {
                            if (Archivos[j].Lista[l].Tabs == tabs)
                            {
                                seguir = false;
                                k = l;
                            }
                        }


                        if (!seguir)
                        {
                            // encontrar los hijos del hermano
                            seguir = true;


                            hijos = 0;
                            for (l = k + 1; l < n && seguir; l++)
                            {
                                if (Archivos[j].Lista[l].Tabs > tabs)
                                {
                                    seguir = true;
                                    hijos++;
                                }
                                else
                                { seguir = false; }
                            }

                            // mover el hermano hacia arriba.
                            nuevaSeleccion = MoverItemArriba(k, j, n);
                            //enfocar el actual
                            if (nuevaSeleccion > -1) EnfocarItem(j, nuevaSeleccion + 1 + hijos, n);

                        }


                    }

                }
            }

        }

        /// <summary>
        /// movimiento lateral, Agrega tabulación al item
        /// </summary>
        /// <param name="i"> Número de item</param>
        /// <param name="index">índice de archivos </param>
        /// <param name="n"> cantidad de items de ese archivo</param>
        private void MoverItemsDerecha(int i, int index, int n)
        {
            int k, l, tabs;
            int TabActual;
            Boolean seguir;



            TabActual = Archivos[index].Lista[i].Tabs;


            if ((i - 1) >= 0)
            {
                if (Archivos[index].Lista[i - 1].Tabs > Archivos[index].Lista[i].Tabs)
                {

                    tabs = Archivos[index].Lista[i].Tabs + 1;
                    Archivos[index].Lista[i].Tabs = tabs;
                    Archivos[index].Lista[i].Objeto.Left = 50 * tabs - Archivos[index].Pestaña.HorizontalScroll.Value + ItemTabCero;
                    //MOVER HIJOS

                    l = 0;
                    seguir = true;
                    for (k = i + 1; k < n && seguir; k++)
                    {
                        if (Archivos[index].Lista[k].Tabs > TabActual) l = k;
                        else seguir = false;
                    }



                    for (k = i + 1; k <= l; k++)
                    {
                        tabs = Archivos[index].Lista[k].Tabs + 1;
                        Archivos[index].Lista[k].Tabs = tabs;
                        Archivos[index].Lista[k].Objeto.Left = 50 * tabs - Archivos[index].Pestaña.HorizontalScroll.Value + ItemTabCero;
                    }





                }
                else if (Archivos[index].Lista[i - 1].Tabs == Archivos[index].Lista[i].Tabs)
                {

                    tabs = Archivos[index].Lista[i - 1].Tabs + 1;
                    Archivos[index].Lista[i].Tabs = tabs;
                    Archivos[index].Lista[i].Objeto.Left = 50 * tabs - Archivos[index].Pestaña.HorizontalScroll.Value + ItemTabCero;
                    //MOVER HIJOS
                    l = 0;
                    seguir = true;
                    for (k = i + 1; k < n && seguir; k++)
                    {
                        if (Archivos[index].Lista[k].Tabs > TabActual) l = k;
                        else seguir = false;
                    }



                    for (k = i + 1; k <= l; k++)
                    {
                        tabs = Archivos[index].Lista[k].Tabs + 1;
                        Archivos[index].Lista[k].Tabs = tabs;
                        Archivos[index].Lista[k].Objeto.Left = 50 * tabs - Archivos[index].Pestaña.HorizontalScroll.Value + ItemTabCero;
                    }

                    HuboCambio();
                }
            }


        }

        /// <summary>
        /// movimiento lateral, Quita tabulación al item
        /// </summary>
        /// <param name="i"> Número de item</param>
        /// <param name="index">índice de archivos </param>
        /// <param name="n"> cantidad de items de ese archivo</param>
        private void MoverItemsIzquierda(int i, int index, int n)
        {
            int j, k, l, tabs;
            int TabsActual;
            Boolean seguir;
            TabsActual = Archivos[index].Lista[i].Tabs;


                        

            // Buscar Padre....

            for (j = i - 1; j > -1 && Archivos[index].Lista[j].Tabs >= TabsActual; j--) ;


            if (j > -1)
            {



                tabs = (Archivos[index].Lista[j].Tabs);
                Archivos[index].Lista[i].Tabs = tabs;
                Archivos[index].Lista[i].Objeto.Left = 50 * tabs - Archivos[index].Pestaña.HorizontalScroll.Value + ItemTabCero;

                //MOVER HIJOS
                l = 0;
                seguir = true;
                for (k = i + 1; k < n && seguir; k++)
                {
                    if (Archivos[index].Lista[k].Tabs > TabsActual) l = k;
                    else seguir = false;
                }
                for (k = i + 1; k <= l; k++)
                {
                    tabs = Archivos[index].Lista[k].Tabs - 1;
                    Archivos[index].Lista[k].Tabs = tabs;
                    Archivos[index].Lista[k].Objeto.Left = 50 * tabs - Archivos[index].Pestaña.HorizontalScroll.Value + ItemTabCero;
                }
                HuboCambio();

            }



        }


        /// <summary>
        /// Boton '>' para mover el item a la derecha y agregar tabulación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Derecha_Click(object sender, EventArgs e)
        {
            int i, index, n;


            if (Archivos.Count > 0)
            {

                index = Pestañas.SelectedIndex;

                n = Archivos[index].Lista.Count;

                if (n > 0)
                {
                    i = Archivos[index].SelectedIndex;

                    if (i >= 0 && i < n)
                    {

                        MoverItemsDerecha(i, index, n);
                        this.PerformLayout();
                    }

                }
            }
        }


        /// <summary>
        /// Boton '<' para mover el item a la izquierda y sacar tabulación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Izquierda_Click(object sender, EventArgs e)
        {
            int i, index, n;

            if (Archivos.Count > 0)
            {


                index = Pestañas.SelectedIndex;

                n = Archivos[index].Lista.Count;

                if (n > 0)
                {
                    i = Archivos[index].SelectedIndex;


                    if (i >= 0 && i < n)
                    {

                        MoverItemsIzquierda(i, index, n);
                        this.PerformLayout();

                    }

                }

            }
        }

        /// <summary>
        /// Función interna para otras funciones
        /// </summary>
        /// <param name="unidades">cantidad de posiciones que se moveran los items, los items tienen una posición física discreta según su orden en la lista</param>
        /// <param name="posicion"> posición dede dónde se correran los items debajo</param>
        private void CorrerTodoDebajo(int unidades,int posicion)
        {
            int index,  j, n;

            index = Pestañas.SelectedIndex;
            n = Archivos[index].Lista.Count;

            if (posicion < n)
            {



                for (j = posicion + 1; j < n; j++)
                {
                    punto = Archivos[index].Lista[j].Objeto.Location;
                    punto.Y += posItem*unidades;
                    Archivos[index].Lista[j].Objeto.Location = punto;
                }



            }


        }


        /// <summary>
        /// Borrar un item específico y acomodar el vacío que este deja en la pestaña
        /// </summary>
        private void BorrarItem()
        {
            int i, n, j, k, actualtab, tabs;



            if (Archivos.Count > 0)
            {

                k = this.Pestañas.SelectedIndex;

                //this.label1.Text = hashcode.ToString();

                n = Archivos[k].Lista.Count;


                i = Archivos[k].SelectedIndex;


                if (i >= 0 && i < n)
                {

                    Archivos[k].Pestaña.SuspendLayout();
                    this.SuspendLayout();
                    //-------

                    CorrerTodoDebajo(-1, i);
                    Archivos[k].Lista[i].Objeto.Dispose();


                    Archivos[k].Lista.RemoveAt(i);

                    Archivos[k].Pestaña.ResumeLayout(false);
                    Archivos[k].Pestaña.PerformLayout();
                    this.ResumeLayout(false);
                    Archivos[k].SelectedIndex--;

                    n = n - 1;
                    if (Archivos[k].SelectedIndex > -1)
                    {

                       
                    }
                    else

                    if (n > 0)
                    {


                        actualtab = Archivos[k].Lista[i].Tabs;






                        if ((i == 0) && (actualtab > 0))
                        {

                            // si el item de arriba de todos no tiene tabs=0, se corre el item
                            j = 0;


                            while (j < n && Archivos[k].Lista[j].Tabs >= actualtab)
                            {
                                tabs = Archivos[k].Lista[j].Tabs;

                                tabs -= actualtab;
                                Archivos[k].Lista[j].Tabs = tabs;

                                Archivos[k].Lista[j].Objeto.Left = 50 * tabs - Archivos[k].Pestaña.HorizontalScroll.Value + ItemTabCero;
                                j++;
                            }
                           

                        }

                    }

                    HuboCambio();
                }


            }




        }



        /// <summary>
        /// Botón de borrar del programa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BorrarClick(object sender, EventArgs e)
        {
            int k, n,selected;



            BorrarItem();



            k = Pestañas.SelectedIndex;
            n = Archivos[k].Lista.Count;
            selected = Archivos[k].SelectedIndex;
            if(n>0 && selected>-1) EnfocarItem(k, selected, n);

        }


        /// <summary>
        /// Nuevo item, botón del programa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NuevoItem_Click(object sender, EventArgs e)
        {
            int i, j;
            if (Archivos.Count > 0)
            {
                i = Pestañas.SelectedIndex;
                Archivos[i].SelectedIndex=AgregarBoton(false, "", -1,0);
                if (!guardarToolStripMenuItem.Enabled)
                {
                    guardarToolStripMenuItem.Enabled = true;
                   

                    //guardarToolStripMenuItem.PerformLayout();
                }
               
                j = Archivos[i].Lista.Count;
                HuboCambio();
                if (Archivos[i].SelectedIndex<0){ 
              
                


                Archivos[i].SelectedIndex = j;
                EnfocarItem(i, j-1, j);
                }
                else
                {
                    //Archivos[i].SelectedIndex++;
                    EnfocarItem(i, Archivos[i].SelectedIndex, j);
                }
                

                
            }
        }

        /// <summary>
        /// Botón existente en el menú Archivo: Nuevo Archivo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NuevoArchivo_Click(object sender, EventArgs e)
        {



            this.panel2.SuspendLayout();
            this.Pestañas.SuspendLayout();
            this.SuspendLayout();



            Nuevo_Archivo("");
            ventanaGuardada = false;
            VentanaEstaGuardada();
            this.panel2.ResumeLayout(false);
            this.Pestañas.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Esta función sirve para verificar que no se abra dos veces el mismo archivo en el programa
        /// </summary>
        /// <param name="direccion"></param>
        /// <returns></returns>
        private Boolean EstaEsteArchivoAbierto(string direccion)
        {
            int i, n;
            Boolean retorno;
            retorno = false;
            n = Archivos.Count;
            if (n > 0)
                for (i = 0; i < n; i++)
                    if (Archivos[i].Direccion == direccion) retorno = true;
            return retorno;

        }

        /// <summary>
        /// Abre un archivo del tipo Andy ToDo List. Cuyo formato es
        /// #B#  
        /// #1# texto item1#P#
        /// #0# texto item2#P#
        /// #E#
        /// Donde #B#:Begin Block; #0#:checkbox=false; #1#: checkbox=true; #P#:Fin de párrafo; E:End Block
        /// Cada bloque tiene una tabulación, un bloque dentro de un bloque tiene una tabulación mayor
        /// </summary>
        /// <param name="direccion"></param>
        /// <returns></returns>
        private Boolean AbrirArchivoEn(string direccion)
        {
            StreamReader archivo;
            int posicion, intChar, nivel, tabulacion;
            bool leyendo, estadoCheck;
            char caracter, opcion;
            string leido;
            Boolean respuesta;

            //abrir

            int i;

            tabulacion = -1;


            nivel = 0;
            opcion = 'N';
            leyendo = false;
            estadoCheck = false;
            leido = "";

            respuesta = false;


            if (!EstaEsteArchivoAbierto(direccion))
            {

                //"D:\\Paginas\\basura\\borrar.txt"
                archivo = null;
                if (File.Exists(direccion)) archivo = new StreamReader(direccion);
                if (archivo != null)
                {
                    Nuevo_Archivo(direccion);
                    Pestañas.SelectedIndex = Pestañas.TabCount - 1;
                    //Pestañas.PerformLayout();
                    i = Pestañas.SelectedIndex;

                    if (Archivos.Count > 0)
                    {
                        if (Archivos[i].Guardado == 0)
                        {
                            Archivos[i].Guardado = 1;
                            guardarToolStripMenuItem.Enabled = false;
                            //guardarToolStripMenuItem.PerformLayout();
                            VentanaEstaGuardada();
                        }
                    }


                    posicion = -1;
                    intChar = archivo.Read();
                    while (intChar != -1)
                    {
                        posicion += 1;
                        caracter = (char)intChar;



                        switch (nivel)
                        {
                            case 0:
                                if (caracter == '#')
                                    nivel = 1;
                                else
                                {
                                    if (leyendo) leido += caracter;
                                }
                                break;
                            case 1:
                                if (caracter == '-')
                                    nivel = 2;
                                else nivel = 0;
                                break;
                            case 2:
                                if ((caracter == 'B' || caracter == 'E' || caracter == '1' || caracter == '0' || caracter == 'P'))

                                {
                                    nivel = 3;
                                    opcion = caracter;
                                }
                                else
                                {
                                    nivel = 0;

                                }
                                break;
                            case 3:
                                if (caracter == '-')
                                    nivel = 4;
                                else nivel = 0;
                                break;
                            case 4:
                                if (caracter == '#')
                                {
                                    switch (opcion)
                                    {
                                        case 'B'://AGREGAR TITULO y POSICION
                                            tabulacion++;
                                            break;
                                        case '1':
                                            if (leyendo)
                                            {
                                                AgregarBoton(estadoCheck, leido, tabulacion,0);
                                            }
                                            leido = "";
                                            estadoCheck = true;
                                            leyendo = true;



                                            break;
                                        case '0':
                                            if (leyendo)
                                            {
                                                AgregarBoton(estadoCheck, leido, tabulacion,0);
                                            }
                                            leido = "";
                                            estadoCheck = false;
                                            leyendo = true;
                                            break;
                                        case 'P':
                                            if (leyendo)
                                            {
                                                AgregarBoton(estadoCheck, leido, tabulacion,0);
                                            }
                                            leido = "";
                                            estadoCheck = false;
                                            leyendo = false;
                                            break;
                                        case 'E':
                                            tabulacion--;
                                            break;
                                        default:
                                            break;
                                    }
                                    nivel = 0;
                                    opcion = 'N';
                                }
                                else
                                {
                                    nivel = 0;
                                    opcion = 'N';
                                }
                                break;

                            default:
                                if (leyendo) leido += caracter;
                                break;
                        }






                        intChar = archivo.Read();

                    }

                    archivo.Close();
                    respuesta = true;



                }
            }

            return respuesta;
        }


        /// <summary>
        /// Botón Abrir Archivo, usa el sistema de windows para abrir archivos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AbrirArchivo_Click(object sender, EventArgs e)
        {
            DialogResult resultado;
            resultado = abrirArchivoDialog.ShowDialog();


            if (resultado == DialogResult.OK)
            {
                this.SuspendLayout();
                if (AbrirArchivoEn(abrirArchivoDialog.FileName))
                {
                    ventanaGuardada = false;
                    VentanaEstaGuardada();
                }
                this.ResumeLayout();
                this.PerformLayout();
            }
        }


        /// <summary>
        /// Guardar archivo con el formato: 
        /// #B#  
        /// #1# texto item1#P#
        /// #0# texto item2#P#
        /// #E#
        /// Donde #B#:Begin Block; #0#:checkbox=false; #1#: checkbox=true; #P#:Fin de párrafo; E:End Block
        /// Cada bloque tiene una tabulación, un bloque dentro de un bloque tiene una tabulación mayor
        /// </summary>
        /// <param name="direccion"></param>
        /// <returns></returns>
        private int GuardarArchivoEn(string direccion)
        {
            StreamWriter archivo;





            int auxiliar = 0;
            int i;
            int tabulacion;


            tabulacion = 0;



            i = this.Pestañas.SelectedIndex;



            auxiliar = Archivos[i].Lista.Count;






            archivo = new StreamWriter(direccion);


            if (archivo != null)
            {

                archivo.WriteLine("#-B-#");


                if (auxiliar > 0) foreach (Nodo ITEM in Archivos[i].Lista)
                    {


                        {
                            if (ITEM.Tabs > tabulacion)
                            {
                                tabulacion++;
                                archivo.WriteLine("#-B-#");
                            }
                            else if (ITEM.Tabs < tabulacion)
                            {
                                tabulacion--;
                                if (tabulacion > -1) archivo.WriteLine("#-E-#");
                                else tabulacion = 0;

                            }
                            System.Windows.Forms.TextBox txxt = ITEM.Objeto.Controls.OfType<System.Windows.Forms.TextBox>().FirstOrDefault();
                            CheckBox chhk = ITEM.Objeto.Controls.OfType<CheckBox>().FirstOrDefault();

                            if (chhk.Checked == true)
                            {
                                archivo.Write("#-1-#");
                            }
                            else
                                archivo.Write("#-0-#");
                            archivo.Write(txxt.Text);
                            archivo.WriteLine("#-P-#");
                        }
                    }

                archivo.WriteLine("#-E-#");

                archivo.Close();

                {
                    Archivos[i].Guardado = 1;
                    guardarToolStripMenuItem.Enabled = false;
                    //guardarToolStripMenuItem.PerformLayout();
                    VentanaEstaGuardada();

                    return 1;

                }

            }



            return 0;
        }

        /// <summary>
        /// Botón guardar Archivo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GuardarArchivo_Click(object sender, EventArgs e)
        {
            int i;
            i = Pestañas.SelectedIndex;
            if (Archivos.Count > 0)
            {
                if (Archivos[i].Direccion != "" && Archivos[i].Direccion != "none")
                    GuardarArchivoEn(Archivos[i].Direccion);
                else GuardarArchivoComo();
                this.PerformLayout();
            }


        }

        /// <summary>
        /// Guardar Archivo Como, abre el sistema de windows para guardar como
        /// </summary>
        /// <returns></returns>
        private int GuardarArchivoComo()
        {
            DialogResult resultado;

            string direccion;
            int i;
            i = Pestañas.SelectedIndex;

            guardarArchivoDialog.FileName = Archivos[i].Nombre;

            resultado = guardarArchivoDialog.ShowDialog();





            if (resultado == DialogResult.OK)
            {
                direccion = guardarArchivoDialog.FileName;


                GuardarArchivoEn(direccion);

                Archivos[i].Direccion = direccion;
                Archivos[i].Nombre = direccion.Substring(direccion.LastIndexOf('\\') + 1);
                Archivos[i].Pestaña.Text = Archivos[i].Nombre;
                //Archivos[i].Pestaña.PerformLayout();

                ventanaGuardada = false;
                VentanaEstaGuardada();

                return 1;
            }

            return 0;
        }


        /// <summary>
        /// Boton guardar archivo como
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GuardarArchivoComo_Click(object sender, EventArgs e)
        {
            GuardarArchivoComo();
            this.PerformLayout();
        }

        /// <summary>
        /// Fuerza el cierre de un archivo sin considerar si han sido guardados. 
        /// </summary>
        /// <param name="i">indice del archivo</param>
        private void CerrarArchivoSinPreguntar(int i)
        {



            Archivos[i].Pestaña.Dispose();
            Archivos.RemoveAt(i);
            //Pestañas.PerformLayout();
        }


        /// <summary>
        /// Se verifica si el archivo ha sido guardado antes de cerrarlo y pregunta al usuario si quiere guardarlo
        /// </summary>
        /// <returns></returns>
        private Boolean PreguntarYCerrarArchivo()
        {
            int i;
            Boolean respuesta;
            DialogResult resultado;

            i = Pestañas.SelectedIndex;
            respuesta = false;

            if (Archivos.Count > 0)
            {
                if (Archivos[i].Guardado == 1)
                {
                    Archivos[i].Pestaña.Dispose();
                    Archivos.RemoveAt(i);
                    //Pestañas.PerformLayout();
                    respuesta = true;
                }

                else
                {

                    resultado = MessageBox.Show("¿Desea guardar los cambios en \"" + Archivos[i].Nombre + "\" ?", "Cerrando Archivo:", MessageBoxButtons.YesNoCancel);
                    if (resultado == DialogResult.Yes)
                    {
                        if (Archivos[i].Direccion != "" && Archivos[i].Direccion != "none")
                        {
                            if (GuardarArchivoEn(Archivos[i].Direccion) == 1)
                            {

                                Archivos[i].Pestaña.Dispose();
                                Archivos.RemoveAt(i);
                                //Pestañas.PerformLayout();
                            }
                        }
                        else
                        {
                            if (GuardarArchivoComo() == 1)
                            {

                                Archivos[i].Pestaña.Dispose();
                                Archivos.RemoveAt(i);
                                // Pestañas.PerformLayout();
                            }
                        }

                        respuesta = true;
                    }
                    else if (resultado == DialogResult.No)
                    {
                        Archivos[i].Pestaña.Dispose();
                        Archivos.RemoveAt(i);
                        //Pestañas.PerformLayout();
                        respuesta = true;
                    }



                }
            }

            return respuesta;

        }

        /// <summary>
        /// Cierra el archivo preguntando si desea guardar si es que no ha sido guardado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CerrarArchivo_Click(object sender, EventArgs e)
        {
            if (PreguntarYCerrarArchivo())
            {
                ventanaGuardada = false;
                VentanaEstaGuardada();
            }
            this.PerformLayout();
        }


        /// <summary>
        ///  ante un cambio anula la bandera "Guardado" del archivo actualmente abierto.
        /// </summary>
        void HuboCambio()
        {
            int i;
            i = Pestañas.SelectedIndex;
            if (Archivos[i].Guardado == 1)
            {
                Archivos[i].Guardado = 0;
                guardarToolStripMenuItem.Enabled = true;
                //guardarToolStripMenuItem.PerformLayout();

            }
        }


   /// <summary>
   /// Ante un cambio en un item, el archivo pierde su caracter de "ya guardado"
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
        private void CambioNodo(object sender, EventArgs e)
        {
            HuboCambio();
        }



        /// <summary>
        /// Hace que el botón de guardar esté o no disponible dependiendo si el archivo con el que se trabaja está o no guardado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pestañas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i;
            i = Pestañas.SelectedIndex;
            if (Archivos[i].Guardado == 0)
            {
                guardarToolStripMenuItem.Enabled = true;
                //guardarToolStripMenuItem.PerformLayout();

            }
            else
            {
                guardarToolStripMenuItem.Enabled = false;
                //guardarToolStripMenuItem.PerformLayout();
            }
        }


        /// <summary>
        /// Abre una ventana indicando créditos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void About_Click(object sender, EventArgs e)
        {
            string cadena;

            cadena = "AS: ♦Andy's Software♦  \r\n\r\n\r\nAndy ToDo List Checker versión " + versionATDLC + "\r\n==================================\r\n\r\n2024 Lite version\r\n\r\n\r\nHecho por Andrés Hugo Bernárdez\r\n\r\n"+fechaVersion+" ----------\r\n Actualización: \r\n "+AboutActualizacion+" \r\nContacto: andreshugobernardez@gmail.com\r\n ";




            MessageBox.Show(cadena /*"Hecho por Andrés Hugo Bernárdez\n 2024\n\n andreshugobernardez@gmail.com"*/, "Andy ToDo List Checker " + versionATDLC);
        }


        /// <summary>
        /// Lista de Archivos Auxiliar para guardar y cerrar archivos ya  guardados
        /// </summary>
        List<string> ListaDeArchivos = new List<string>();


        /// <summary>
        /// Función para llenar lista de archivos con archivos que ya están guardados
        /// </summary>
        private void LlenarListaDeArchivos()
        {


            int i, n, k;
            DialogResult resultado;


            n = ListaDeArchivos.Count;

            for (i = 0; i < n; i++)
            {
                ListaDeArchivos.RemoveAt(0);
            }

            k = Archivos.Count;
            for (i = 0; i < k; i++)
            {
                Pestañas.SelectedIndex = i;
                //Pestañas.PerformLayout();
                if (Archivos[i].Direccion == "" || Archivos[i].Direccion == "none")
                {

                    resultado = MessageBox.Show("¿Desea guardar el archivo \"" + Archivos[i].Nombre + "\" para guardarlo a la Ventana?", "Guardando Archivos en Ventana:", MessageBoxButtons.YesNoCancel);

                    if (resultado == DialogResult.Yes)
                    {

                        GuardarArchivoComo();
                        ListaDeArchivos.Add(Archivos[i].Direccion);
                        Archivos[i].Status = 1;


                    }
                    if (resultado == DialogResult.No)
                    {
                        Archivos[i].Status = 1;
                    }
                    if (resultado == DialogResult.Cancel)
                    {
                        Archivos[i].Status = -1;
                    }



                }
                else if (Archivos[i].Guardado == 0)
                {
                    resultado = MessageBox.Show("¿Desea guardar el archivo \"" + Archivos[i].Nombre + "\" ?", "Guardando Archivos en Ventana:", MessageBoxButtons.YesNoCancel);
                    if (resultado == DialogResult.Yes)
                    {

                        GuardarArchivoEn(Archivos[i].Direccion);
                        ListaDeArchivos.Add(Archivos[i].Direccion);
                        Archivos[i].Status = 1;


                    }
                    if (resultado == DialogResult.No)
                    {
                        Archivos[i].Status = 1;
                    }
                    if (resultado == DialogResult.Cancel)
                    {
                        Archivos[i].Status = -1;
                    }
                }
                else
                {
                    ListaDeArchivos.Add(Archivos[i].Direccion);
                    Archivos[i].Status = 1;

                }
            }







            /*
            int i,n;
            n = ListaDeArchivos.Count;
            for (i = 0; i < n; i++)
            {
                ListaDeArchivos.RemoveAt(0);
            }
            for (i = 0; i < Archivos.Count; i++)
            {
                ListaDeArchivos.Add(Archivos[i].Direccion);
            }*/

        }


        /// <summary>
        /// Función que usa el sistema de windows para guardar la ventana
        /// Una ventana es un TabControl con varios archivos abiertos, uno en cada pestaña.
        /// Esto permite crear distintas ventanas con distintos archivos TO DO list
        /// </summary>
        /// <returns></returns>
        private Boolean GuardarVentanaComoEn()
        {
            DialogResult resultado;
            Boolean returned;


            returned = false;
            if (ListaDeArchivos.Count > 0)
            {



                resultado = guardarVentanaDialog.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    direccionVentana = guardarVentanaDialog.FileName;
                    returned = GuardarVentanaEn(direccionVentana);


                }






            }
            else MessageBox.Show("No se puede guardar Ventana vacía", "Recordatorio:");

            return returned;
        }

        /// <summary>
        /// Permite guardar una ventana
        /// Una ventana es un TabControl con varios archivos abiertos, uno en cada pestaña.
        /// Esto permite crear distintas ventanas con distintos archivos TO DO list
        /// </summary>
        /// <param name="direccion"></param>
        /// <returns></returns>
        private Boolean GuardarVentanaEn(string direccion)
        {
            StreamWriter archivo;
            int i, k;
            string cadena;
            Boolean returned;
            returned = false;

            if (ListaDeArchivos.Count > 0)
            {

                {

                    k = 0;
                    foreach (string direccionAux in ListaDeArchivos)
                    {
                        if (direccionAux == "none" || direccionAux == "")
                            k++;

                    }
                    if (k == ListaDeArchivos.Count)
                        MessageBox.Show("No se puede guardar Ventana vacía, ninguno de los archivos está guardado.", "Recordatorio:");
                    else
                    {




                        archivo = new StreamWriter(direccion);
                        if (archivo != null)
                        {

                            for (i = 0; i < ListaDeArchivos.Count; i++)
                            {
                                cadena = ListaDeArchivos[i];
                                if (cadena != "" && cadena != "none") archivo.WriteLine(cadena);
                            }
                        }
                        archivo.Close();
                        ventanaGuardada = true;
                        VentanaEstaGuardada();
                        direccionVentana = direccion;
                        nombreVentana = direccionVentana.Substring((direccionVentana.LastIndexOf("\\")) + 1);
                        this.Text = programaNombre + ": " + nombreVentana;
                        //this.PerformLayout();

                        returned = true;

                    }
                }
            }
            else MessageBox.Show("No se puede guardar Ventana vacía", "Recordatorio:");


            return returned;
        }


        /// <summary>
        /// BOton para guardar la ventana
        /// Una ventana es un TabControl con varios archivos abiertos, uno en cada pestaña.
        /// Esto permite crear distintas ventanas con distintos archivos TO DO list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GuardarVentana_Click(object sender, EventArgs e)
        {
            if (!ventanaGuardada)
            {
              
                LlenarListaDeArchivos();
                if (direccionVentana == "none" || direccionVentana == "")
                    GuardarVentanaComoEn();

                else GuardarVentanaEn(direccionVentana);

            }

        }

        /// <summary>
        /// Boton para guardar ventana como
        /// Una ventana es un TabControl con varios archivos abiertos, uno en cada pestaña.
        /// Esto permite crear distintas ventanas con distintos archivos TO DO list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GuardarVentanaComo_Click(object sender, EventArgs e)
        {
            LlenarListaDeArchivos();
            GuardarVentanaComoEn();


        }




        /*
        private void CerrarVentanaYa()
        {
            int i, n;

            n = Archivos.Count;
            if(n>0)
            {
                for(i=n-1;i>-1; i--)
                {
                    Pestañas.SelectedIndex=i;
                    Pestañas.PerformLayout();

                    CerrarArchivoSinPreguntar();
                }
                direccionVentana = "none";
                ventanaGuardado = false;
                ventanaEstaGuardado();
            }

            


        }

        */


        /// <summary>
        /// Cierra todos los archivos que ya están guardados
        /// </summary>
        /// <returns></returns>
        private Boolean CerrarArchivosGuardados()
        {
            int i, n;
            Boolean retorno;
            n = Archivos.Count;
            retorno = true;
            for (i = n - 1; i > -1; i--)
            {
                if (Archivos[i].Status != -1)
                {

                    CerrarArchivoSinPreguntar(i);
                }
                else
                {
                    Archivos[i].Status = 1;
                }
            }
            if (Archivos.Count > 0)
            {
                ventanaGuardada = false;
                VentanaEstaGuardada();
                retorno = false;
            }
            return retorno;
        }

        /// <summary>
        /// Pregunta al usuario si quiere guardar la ventana antes de cerrarla
        /// Una ventana es un TabControl con varios archivos abiertos, uno en cada pestaña.
        /// Esto permite crear distintas ventanas con distintos archivos TO DO list
        /// </summary>
        /// <returns></returns>
        private Boolean PreguntarYCerrarVentana()
        {
            DialogResult respuesta;

            Boolean returned;

            returned = false;




            if (ventanaGuardada)
            {
                LlenarListaDeArchivos();


                // se cierra la ventana.
                direccionVentana = "none";
                nombreVentana = "";


                //se cierran los archivos
                CerrarArchivosGuardados();
                this.Text = programaNombre;
                // this.PerformLayout();
                returned = true;
            }
            else
            {

                if (direccionVentana != "none" && direccionVentana != "")
                {



                    respuesta = MessageBox.Show("¿Desea guardar la ventana \"" + direccionVentana + "\" antes de cerrarlo?", "Cerrar ventana:", MessageBoxButtons.YesNoCancel);

                    if (respuesta == DialogResult.Yes)
                    {
                        LlenarListaDeArchivos();

                        if (GuardarVentanaEn(direccionVentana))
                        {
                            direccionVentana = "none";
                            ventanaGuardada = true;
                            nombreVentana = "";
                            VentanaEstaGuardada();
                            nombreVentana = "";
                            returned = true;
                            this.Text = programaNombre;
                            //this.PerformLayout();
                        }
                        CerrarArchivosGuardados();
                    }
                    else if (respuesta == DialogResult.No)
                    {
                        LlenarListaDeArchivos();


                        direccionVentana = "none";
                        nombreVentana = "";
                        ventanaGuardada = true;
                        VentanaEstaGuardada();
                        nombreVentana = "";
                        returned = true;
                        this.Text = programaNombre;
                        // this.PerformLayout();
                        CerrarArchivosGuardados();

                    }

                }
                else
                {
                    respuesta = MessageBox.Show("¿Desea guardar esta nueva ventana antes de cerrarlo?", "Cerrar ventana:", MessageBoxButtons.YesNoCancel);
                    if (respuesta == DialogResult.Yes)
                    {

                        LlenarListaDeArchivos();

                        if (GuardarVentanaComoEn())
                        {
                            direccionVentana = "none";
                            nombreVentana = "";
                            ventanaGuardada = true;
                            VentanaEstaGuardada();
                            returned = true;
                            this.Text = programaNombre;
                            this.PerformLayout();
                        }
                        CerrarArchivosGuardados();
                    }
                    else if (respuesta == DialogResult.No)
                    {
                        LlenarListaDeArchivos();
                        direccionVentana = "none";
                        nombreVentana = "";
                        ventanaGuardada = true;
                        VentanaEstaGuardada();
                        returned = true;
                        this.Text = programaNombre;
                        this.PerformLayout();
                        CerrarArchivosGuardados();
                    }




                }

            }


            return returned;
        }


        /// <summary>
        /// Boton para cerrar la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CerrarVentana_Click(object sender, EventArgs e)
        {
            int i, n;

           
            if (Archivos.Count > 0)
            {
                PreguntarYCerrarVentana();

            }
            else
            {
                direccionVentana = "none";
                ventanaGuardada = true;
                VentanaEstaGuardada();
                n = ListaDeArchivos.Count;
                for (i = 0; i < n; i++)
                {
                    ListaDeArchivos.RemoveAt(0);
                }
                nombreVentana = "";
                this.Text = programaNombre;
                this.PerformLayout();
            }


        }


        /// <summary>
        /// Permite abrir una ventana, usa el sistema de Windows
        /// Una ventana es un TabControl con varios archivos abiertos, uno en cada pestaña.
        /// Esto permite crear distintas ventanas con distintos archivos TO DO list
        /// </summary>
        /// <returns></returns>
        private Boolean AbrirVentanaEn()
        {
            DialogResult resultado;
            StreamReader archivo;
            string cadena;
            Boolean returned;

            returned = false;
            resultado = abrirVentanaDialog.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                if (PreguntarYCerrarVentana())
                {


                    direccionVentana = abrirVentanaDialog.FileName;
                    archivo = null;
                    if (File.Exists(direccionVentana)) archivo = new StreamReader(direccionVentana);
                    if (archivo != null)
                    {
                        while ((cadena = archivo.ReadLine()) != null)
                        {
                            if (File.Exists(cadena))
                                if (AbrirArchivoEn(cadena)) returned = true;

                        }

                        if (returned)
                        {
                            ventanaGuardada = true;
                            VentanaEstaGuardada();
                            nombreVentana = direccionVentana.Substring((direccionVentana.LastIndexOf("\\")) + 1);
                            this.Text = programaNombre + ": " + nombreVentana;
                            //this.PerformLayout();
                        }

                        archivo.Close();
                    }

                }
            }
            return returned;
        }


        /// <summary>
        /// Boton Abrir Ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AbrirVentana_Click(object sender, EventArgs e)
        {
            


            this.SuspendLayout();

            AbrirVentanaEn();
            this.ResumeLayout();
            this.PerformLayout();



        }





        /// <summary>
        /// Cada vez que se selecciona un Item se pone rojo
        /// </summary>
        /// <param name="index">indice del archivo actual</param>
        /// <param name="j">indice del item actual</param>
        /// <param name="n">Cantidad de items que tiene el archivo actual, usado para optimizar velocidad</param>
        private void EnfocarItem(int index, int j,int n)
        {
            int i;


            Archivos[index].SelectedIndex = j;
            
            /*Archivos[index].Lista[j].Objeto.BorderStyle = BorderStyle.Fixed3D;*/
            Archivos[index].Lista[j].Objeto.Refresh();
            Archivos[index].Lista[j].Objeto.Focus();

            for(i =0; i < n; i++)
                {
                Archivos[index].Lista[i].Objeto.Refresh();
            }


           /* for (i = 0; i < j; i++)
            {
                if (Archivos[index].Lista[i].Objeto.BorderStyle == BorderStyle.Fixed3D)
                {
                    Archivos[index].Lista[i].Objeto.BorderStyle = BorderStyle.None;
                    Archivos[index].Lista[i].Objeto.Refresh();

                }
            }
            for (i = j + 1; i < n; i++)
            {
                if (Archivos[index].Lista[i].Objeto.BorderStyle == BorderStyle.Fixed3D)
                {
                    Archivos[index].Lista[i].Objeto.BorderStyle = BorderStyle.None;
                    Archivos[index].Lista[i].Objeto.Refresh();

                }
            }*/
        }


        /// <summary>
        /// Usado para seleccionar un item, el item se pondrá rojo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_KeyUp(object sender, KeyEventArgs e)
        {
            int HashCode, i, n, j, index;


            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {

                HashCode = sender.GetHashCode();

                index = Pestañas.SelectedIndex;

                n = Archivos[index].Lista.Count;
                j = -1;

                //Encontrar j
                for (i = 0; i < n; i++)
                {
                    if (Archivos[index].Lista[i].HashObjeto == HashCode)
                    {

                        j = i;

                    }

                }
                if (j > -1)
                {
                    if (e.KeyCode == Keys.Down)
                    {
                        if (j + 1 < n )
                        {
                            j = j + 1;
                            
                            EnfocarItem(index, j,n);
                            


                        }
                    }
                    else if (e.KeyCode == Keys.Up)
                    {
                        if (j - 1 > -1)
                        {
                            j = j - 1;
                           
                            EnfocarItem(index, j,n);
                        }
                    }

                }


            }


        }


        

        /// <summary>
        /// Permite poner un item en rojo dependiendo del selectedindex del archivo
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_Paint(object sender, PaintEventArgs e)
        {
            int hashcode, index=0, indexaux, i, j, n;

            hashcode = sender.GetHashCode();








            j = -1;


            for (indexaux = 0; indexaux < Archivos.Count; indexaux++)
            {
                n = Archivos[indexaux].Lista.Count;
                for (i = 0; i < n; i++)
                {
                    if (hashcode == Archivos[indexaux].Lista[i].Objeto.GetHashCode())
                    {
                        j = i;
                        index = indexaux;
                        i = n;
                        indexaux = Archivos.Count;
                    }
                }
            }

            if (j > -1)
            {



                if (j == Archivos[index].SelectedIndex)
                {
                    // Create pen.
                    Pen ItemPen = new Pen(Color.Red, 3);

                    // Create location and size of rectangle.
                    float x = 1.0F;
                    float y = 1.0F;
                    float width = (float)(Archivos[index].Lista[j].Objeto.Width - 3);
                    float height = (float)(Archivos[index].Lista[j].Objeto.Height - 3);

                    // Draw rectangle to screen.
                    e.Graphics.DrawRectangle(ItemPen, x, y, width, height);

                    Invalidate();
                }
                              


            }

        




        }


        /// <summary>
        /// redirección a GuardarVentana_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guardarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GuardarVentana_Click(sender, e);
        }



        /// <summary>
        /// redirección a AbrirArchivo_Click(sender, e);
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirArchivo_Click(sender, e);
        }
        /// <summary>
        /// redirección a GuardarArchivo_Click(sender, e);
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GuardarArchivo_Click(sender, e);
        }


        /// <summary>
        /// redirección a GuardarArchivoComo_Click(sender, e);
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GuardarArchivoComo_Click(sender, e);
        }

        /// <summary>
        /// redirección a CerrarArchivo_Click(sender, e);
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarArchivo_Click(sender, e);
        }


        /// <summary>
        /// redirección a NuevoItem_Click(sender, e);
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void agregarItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NuevoItem_Click(sender, e);
        }

        /// <summary>
        /// redirección a AbrirVentana_Click(sender, e);
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void abrirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirVentana_Click(sender, e);
        }

        /// <summary>
        /// redirección a GuardarVentanaComo_Click(sender, e);
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guardarComoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GuardarVentanaComo_Click(sender, e);
        }

        /// <summary>
        /// redirección a CerrarVentana_Click(sender, e);
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cerrarVentanaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarVentana_Click(sender, e);
        }

        /// <summary>
        /// redirección a BorrarClick(sender, e);
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void borrarItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BorrarClick(sender, e);
        }
        /// <summary>
        /// redirección a About_Click(sender, e);
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About_Click(sender, e);
        }

        /// <summary>
        /// redirección a 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subirItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Arriba_Click(sender, e);
        }

        /// <summary>
        /// redirección a Abajo_Click(sender, e);
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bajarItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abajo_Click(sender, e);
        }

        /// <summary>
        /// redirección a Derecha_Click(sender, e);
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void correrALaDerechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Derecha_Click(sender, e);
        }

        /// <summary>
        /// redirección a Izquierda_Click(sender, e);
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void correrALaIzquierdaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Izquierda_Click(sender, e);
        }

        /// <summary>
        /// redirección a  Arriba_Click(sender, e);
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoverArriba_Click(object sender, EventArgs e)
        {
            Arriba_Click(sender, e);
        }

        /// <summary>
        /// redirección a Abajo_Click(sender, e);
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoverAbajo_Click(object sender, EventArgs e)
        {
            Abajo_Click(sender, e);
        }

        /// <summary>
        /// redirección a Derecha_Click(sender, e);
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoverDerecha_Click(object sender, EventArgs e)
        {
            Derecha_Click(sender, e);
        }

        /// <summary>
        /// redirección a Izquierda_Click(sender, e);
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoverIzquierda_Click(object sender, EventArgs e)
        {
            Izquierda_Click(sender, e);
        }

        /// <summary>
        /// redirección a BorrarClick(sender, e);
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EliminarItem_Click(object sender, EventArgs e)
        {
            BorrarClick(sender, e);
        }

        /// <summary>
        /// Copia el item seleccionado en rojo
        /// </summary>
        void CopiarItem()
        {
            int index, Seleccionado, n, tab;
            string texto;
            bool estado;
            if (Archivos.Count > 0)
            {
                index = Pestañas.SelectedIndex;
                Seleccionado = Archivos[index].SelectedIndex;
                n = Archivos[index].Lista.Count;
                if (Seleccionado > -1 && Seleccionado < n)
                {
                    tab = Archivos[index].Lista[Seleccionado].Tabs;
                    texto = Archivos[index].Lista[Seleccionado].Texto.Text;
                    estado = Archivos[index].Lista[Seleccionado].Checkin.Checked; ;

                    Copia1.Settear(estado, texto, tab);

                }
            }
        }

        /// <summary>
        /// redirección a CopiarItem();
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopiarItem();
        }

        /// <summary>
        /// copiar y borrar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopiarItem();
            BorrarClick(sender, e);
        }


        /// <summary>
        /// Pega un item en la posición señalada en rojo
        /// </summary>
        private void PegarItem()
        {
            int tabs, i, j; ;
            bool estado;
            string txt;


            tabs = Copia1.Tab;
            estado = Copia1.Estado;
            txt = Copia1.Texto;








            if (Archivos.Count > 0)
            {
                i = Pestañas.SelectedIndex;

                Archivos[i].SelectedIndex = AgregarBoton(estado, txt, tabs,1);
            
                if (!guardarToolStripMenuItem.Enabled)
                {
                    guardarToolStripMenuItem.Enabled = true;


                    //guardarToolStripMenuItem.PerformLayout();
                }
                
                j = Archivos[i].Lista.Count;
                CambioNodo(null, null);

                if (Archivos[i].SelectedIndex < 0)
                {




                    Archivos[i].SelectedIndex = j;
                    EnfocarItem(i, j - 1, j);
                }
                else
                {
                    //Archivos[i].SelectedIndex++;
                    EnfocarItem(i, Archivos[i].SelectedIndex, j);
                }



            }
        }

        /// <summary>
        /// redirección a PegarItem();
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pegarItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PegarItem();
        }

        /// <summary>
        /// redirección a CopiarItem();
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopiarBoton_Click(object sender, EventArgs e)
        {
            CopiarItem();
        }

        /// <summary>
        /// copia y borra
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CortarBoton_Click(object sender, EventArgs e)
        {
            CopiarItem();
            BorrarClick(sender, e);
        }

        /// <summary>
        /// redirección a  PegarItem();
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PegarBoton_Click(object sender, EventArgs e)
        {
            PegarItem();
        }


    }






    /*
     Andrés Hugo Bernárdez 2024 andreshugobernárdez@gmail.com

     */

}
