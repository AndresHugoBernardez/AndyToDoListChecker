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


Andy ToDo List Checker versión 1.0
==================================

2024 Lite version


Hecho por Andrés Hugo Bernárdez

6 de enero del 2024 
 
Contacto: andreshugobernardez@gmail.com
 
 
 
*/








using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace AndyToDoListChecker
{


    


    public partial class Form1 : Form
    {
        
        // Lista de datos
       public class Nodo {

            public bool Estado { get; set; } 
            public string Item { get; set; }
            public int Hasharriba { get; set; }
            public int Hashabajo { get; set; }
            public int Posicion { get; set; }
            public int Hashborrar { get; set; }

            public GroupBox Objeto { get; set; }
            public Panel Botones { get; set; }
            public TextBox Texto { get; set; }

            public Nodo()
            {
                Estado = false;
                Item = "";
            }
            public Nodo(string item)
            {
                Estado = false;
                Item = item;
            }
            public Nodo(bool estado, string item)
            {
                Estado = estado;
                Item = item;
            }
            public Nodo(bool estado, string item, int hasharriba, int hashabajo, int hashborrar, int posicion, GroupBox objeto, TextBox texto, Panel botones)
            {
                Estado = estado;
                Item = item;
                Hasharriba = hasharriba;
                Hashabajo = hashabajo;
                Hashborrar = hashborrar;
                Posicion = posicion;
                Objeto = objeto;
                Texto = texto;
                Botones = botones;
            }
        };



        public class ArchivoNodo
        {
            public string Direccion { get; set; }
            public string Nombre { get; set; }
            public int Guardado { get; set; }

            public int Status { get; set; }

            public TabPage Pestaña { get; set; }

            public List<Nodo> Lista { get; set; } 

            public ArchivoNodo(string direccion, string nombre, int guardado,int indice)
            {
                Direccion = direccion;
                Nombre = nombre;
                Guardado = guardado;
                Status = indice;
            }
        }


        string direccionVentana = "none";
        Boolean ventanaGuardada = true;
        const string programaNombre = "Andy ToDo List Checker";
        string nombreVentana = "";





List<ArchivoNodo> Archivos = new List<ArchivoNodo>();


        private void VentanaEstaGuardada()
        {
       
           
            if (ventanaGuardada)
            {
                guardarVentana.Enabled = false;
                guardarVentana.PerformLayout();
            }
            else
            {
                guardarVentana.Enabled = true;
                guardarVentana.PerformLayout();
            }
        }

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
                foreach (ArchivoNodo archNodo  in Archivos)
                {
                    if (("Nuevo" + j.ToString() + ".atd") == archNodo.Nombre) seguir = false;
                }

            }
            while (!seguir);

            if(Direccion=="" || Direccion=="none")
            Archivos.Add(new ArchivoNodo("none", "Nuevo" + j.ToString()+ ".atd", 0,1));
            else
            {
                posicion=Direccion.LastIndexOf('\\');
                Archivos.Add(new ArchivoNodo(Direccion,Direccion.Substring(posicion+1), 1, 1));
            }


            




            this.panel2.SuspendLayout();
            this.Pestañas.SuspendLayout();
            this.SuspendLayout();


            Archivos[i].Pestaña= new System.Windows.Forms.TabPage();
            Archivos[i].Lista = new List<Nodo>();

            this.Pestañas.Controls.Add(Archivos[i].Pestaña);
            
            Archivos[i].Pestaña.AutoScroll = true;
            Archivos[i].Pestaña.Location = new System.Drawing.Point(4, 22);
            Archivos[i].Pestaña.Name = "tab"+(i+1).ToString();
            Archivos[i].Pestaña.Padding = new System.Windows.Forms.Padding(3);
            Archivos[i].Pestaña.Size = new System.Drawing.Size(644, 359);
            Archivos[i].Pestaña.TabIndex = 0;
            Archivos[i].Pestaña.Text = Archivos[i].Nombre;
            Archivos[i].Pestaña.UseVisualStyleBackColor = true;

            this.Pestañas.SelectedIndex=i;

            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
           

            this.Pestañas.ResumeLayout(false);
            this.ResumeLayout(false);




            

        }





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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            if (ventanaGuardada) e.Cancel = false;
            else
            {
                if (Archivos.Count > 0)
                {
                    PreguntarYCerrarVentana();
                
                }
                if (Archivos.Count == 0) e.Cancel = false;
                
            }
            
            
           
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {

            int i, j, ni, nj;
            Control control = (Control)sender;




            Pestañas.Width = (control.Size.Width) - 130 - 10;
            Pestañas.Height = control.Size.Height - 65;
            panel2.Left = control.Size.Width - 125;
            panel2.Height = control.Size.Height - 65;
            panelVentana.Left = control.Size.Width - 210;
            //  paneles.Width = (control.Size.Width) - 130;
            //  paneles.Height = control.Size.Height - 65;


            ni = Archivos.Count;

            for (i = 0; i < ni; i++)
            {
                nj = Archivos[i].Lista.Count;
                for (j = 0; j < nj; j++)
                {

                    Archivos[i].Lista[j].Objeto.Width = control.Size.Width - 160;
                    Archivos[i].Lista[j].Texto.Width = control.Size.Width - 160 - 110;
                    Archivos[i].Lista[j].Botones.Left = control.Size.Width - 160 - 80;

                }
            }

            this.ResumeLayout();
        }

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            this.SuspendLayout();
        }

        private void RegisterEventHandler()
        {

         
    }
        int maxi = 0;

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

                    Archivos[i].Lista[j].Objeto.Width = control.Size.Width - 160;
                    Archivos[i].Lista[j].Texto.Width = control.Size.Width - 160 - 110;
                    Archivos[i].Lista[j].Botones.Left = control.Size.Width - 160 - 80;

                }
            }
            this.Pestañas.ResumeLayout();
            this.panel2.ResumeLayout();
            this.ResumeLayout();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            


        }



        
        Point punto = new Point(0,0);
        const int CajaAlto= 45;
        const int posItem = 45;
        const int CajitaAlto = 23;
        const int YAlto = 15;

        public void AgregarBoton(Boolean chhhkIN, String txxxtIN)
        {
            int auxiliar = 0;
            int i;

            Boolean chhhk;
            String txxxt;
                chhhk=chhhkIN;
                txxxt = txxxtIN;


            if(Archivos.Count>0)
            { 

                i = this.Pestañas.SelectedIndex;



                auxiliar = Archivos[i].Lista.Count;
                CheckBox chk = new System.Windows.Forms.CheckBox();
                TextBox txt = new System.Windows.Forms.TextBox();
                Button borrar = new System.Windows.Forms.Button();
                Button arriba = new System.Windows.Forms.Button();
                Button abajo = new System.Windows.Forms.Button();
                Panel panel = new System.Windows.Forms.Panel();

           
            

                GroupBox item = new System.Windows.Forms.GroupBox();
                item.SuspendLayout();

                // item1
                // 
                panel.Controls.Add(abajo);
                panel.Controls.Add(arriba);
                panel.Controls.Add(borrar);
                item.Controls.Add(panel);
                item.Controls.Add(txt);
                item.Controls.Add(chk);

            
                item.Location = new System.Drawing.Point(3-Archivos[i].Pestaña.HorizontalScroll.Value,3+auxiliar * posItem- Archivos[i].Pestaña.VerticalScroll.Value);
                item.Name = "item" +i.ToString()+"-"+ auxiliar.ToString();
                item.Size = new System.Drawing.Size(643 + 100, CajaAlto);
                item.TabIndex = 4;
                item.TabStop = false;


                // 
                // chk1
                // 
                chk.AutoSize = true;
                chk.Location = new System.Drawing.Point(7, YAlto);
                chk.Name = "chk" +i.ToString()+"-"+ auxiliar.ToString();
                chk.Size = new System.Drawing.Size(15, 14);
                chk.TabIndex = 1;
                chk.UseVisualStyleBackColor = true;
                chk.Checked = chhhk;
                chk.Click += CambioNodo;
                
                // 
                // txt1
                // 
                txt.Location = new System.Drawing.Point(CajitaAlto, YAlto-7);
                txt.Multiline = true;
                txt.Name = "txt" +i.ToString()+"-"+ auxiliar.ToString();
                txt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
                txt.Size = new System.Drawing.Size(530, CajitaAlto+10);
                txt.TabIndex = 0;
                txt.Text = txxxt;
                txt.TextChanged += CambioNodo;


                //
                // panel
                //


                panel.AutoScroll = false;
                panel.Location = new System.Drawing.Point(584, YAlto);
                panel.Name = "panel2" + i.ToString() + "-" + auxiliar.ToString();
                panel.Size = new System.Drawing.Size(75, CajitaAlto+5);
                panel.TabIndex = 10;


                // 
                // borrar1
                // 
                borrar.Location = new System.Drawing.Point(50, 0);
                borrar.Name = "borrar" +i.ToString()+"-"+ auxiliar.ToString();
                borrar.Size = new System.Drawing.Size(22, CajitaAlto);
                borrar.TabIndex = 4;
                borrar.Text = "X";
                borrar.UseVisualStyleBackColor = true;
                borrar.Click += new System.EventHandler(this.BorrarClick);
                borrar.Click += CambioNodo;

                // 
                // arriba1
                // 
                arriba.Location = new System.Drawing.Point(25, 0);
                arriba.Name = "arriba" +i.ToString()+"-"+ auxiliar.ToString();
                arriba.Size = new System.Drawing.Size(22, CajitaAlto);
                arriba.TabIndex = 3;
                arriba.Text = "^";
                arriba.UseVisualStyleBackColor = true;
                arriba.Click += new System.EventHandler(this.Mover_Click);
                arriba.Click += CambioNodo;
                // 
                // abajo1
                // 
                abajo.Location = new System.Drawing.Point(0, 0);
                abajo.Name = "abajo" +i.ToString()+"-"+ auxiliar.ToString();
                abajo.Size = new System.Drawing.Size(22, CajitaAlto);
                abajo.TabIndex = 2;
                abajo.Text = "v";
                abajo.UseVisualStyleBackColor = true;
                abajo.Click += new System.EventHandler(this.Mover_Click);
                abajo.Click += CambioNodo;




                item.Width = this.ClientSize.Width - 144;
               txt.Width = this.ClientSize.Width - 144 - 110;
               panel.Left = this.ClientSize.Width - 144 - 80;

                //punto.X = 0;
                // punto.Y = this.tab1.VerticalScroll.Value;

                // this.tab1.AutoScrollPosition = punto ;

                Archivos[i].Pestaña.Controls.Add(item);


                item.ResumeLayout(false);
                item.PerformLayout();







                Archivos[i].Lista.Add(new Nodo(false, "item"+auxiliar.ToString(),arriba.GetHashCode(),abajo.GetHashCode(),borrar.GetHashCode(),auxiliar,item,txt,panel));
                /*
                foreach (Nodo NodoA in Lista)
                {
                    this.label1.Text += NodoA.Item;
                }*/

                txt.Focus();
            }

        }


     


        private void Mover_Click(object sender, EventArgs e)
        {
            int i,n,j,salir;
            int hashcode;
            Nodo temporal;
            j = this.Pestañas.SelectedIndex;

            i = 0;
            salir = 0;
            hashcode= sender.GetHashCode();
            //this.label1.Text = hashcode.ToString();

            n = Archivos[j].Lista.Count;
            if (n>1)
            {

                do
                {
                    if (Archivos[j].Lista[i].Hashabajo == hashcode) salir = 1;
                    if (Archivos[j].Lista[i].Hasharriba == hashcode) salir = -1;

                    i++;
                } while (salir == 0 && i < n);


                if(salir==1)
                {
                    if (i < n)
                    {
                        Archivos[j].Pestaña.SuspendLayout();
                        this.SuspendLayout();

                        punto = Archivos[j].Lista[i - 1].Objeto.Location;
                        punto.Y += posItem;
                        Archivos[j].Lista[i - 1].Objeto.Location = punto;
                        punto = Archivos[j].Lista[i].Objeto.Location;
                        punto.Y -= posItem;
                        Archivos[j].Lista[i].Objeto.Location = punto;

                        temporal = Archivos[j].Lista[i - 1];
                        Archivos[j].Lista.RemoveAt(i - 1);
                        Archivos[j].Lista.Insert(i, temporal);
                        Archivos[j].Pestaña.ResumeLayout(false);
                        Archivos[j].Pestaña.PerformLayout();
                        this.ResumeLayout(false);
                        /*
                        this.label1.Text = "";
                        foreach (Nodo nodoA in Lista)
                        {
                            this.label1.Text += nodoA.Posicion.ToString() + "_";

                        }*/


                    }
                }
                else if (salir == -1)
                {
                    if (i >1)
                    {
                        Archivos[j].Pestaña.SuspendLayout();
                        this.SuspendLayout();
                        punto = Archivos[j].Lista[i - 1].Objeto.Location;
                        punto.Y -= posItem;
                        Archivos[j].Lista[i - 1].Objeto.Location = punto;
                        punto = Archivos[j].Lista[i-2].Objeto.Location;
                        punto.Y += posItem;
                        Archivos[j].Lista[i-2].Objeto.Location = punto;

                        temporal = Archivos[j].Lista[i - 1];
                        Archivos[j].Lista.RemoveAt(i - 1);
                        Archivos[j].Lista.Insert(i-2, temporal);
                        Archivos[j].Pestaña.ResumeLayout(false);
                        Archivos[j].Pestaña.PerformLayout();
                        this.ResumeLayout(false);
                        /*
                        this.label1.Text = "";
                        foreach (Nodo nodoA in Lista)
                        {
                            this.label1.Text += nodoA.Posicion.ToString() + "_";

                        }*/

                    }
                }



            }

            
        }



        private void BorrarClick(object sender, EventArgs e)
        {
            int i, n,j,k, salir;
            int hashcode;
            
            i = 0;
            salir = 0;
            hashcode = sender.GetHashCode();

            k = this.Pestañas.SelectedIndex;

            //this.label1.Text = hashcode.ToString();

            n = Archivos[k].Lista.Count;


                do
                {
                    if (Archivos[k].Lista[i].Hashborrar == hashcode) salir = 1;
             

                    i++;
                } while (salir == 0 && i < n);


                if (salir == 1)
                {
                Archivos[k].Pestaña.SuspendLayout();
                this.SuspendLayout();

                if (i < n)
                    {
                        
                        

                        for (j = i; j < n; j++)
                        {
                            punto = Archivos[k].Lista[j].Objeto.Location;
                            punto.Y -= posItem;
                        Archivos[k].Lista[j].Objeto.Location = punto;
                        }



                    }
                Archivos[k].Lista[i - 1].Objeto.Dispose();


                Archivos[k].Lista.RemoveAt(i - 1);

                Archivos[k].Pestaña.ResumeLayout(false);
                Archivos[k].Pestaña.PerformLayout();
                this.ResumeLayout(false);
            }
                    



            

        }


        private void NuevoItem_Click(object sender, EventArgs e)
        {
            AgregarBoton(false,"");
            if (!guardarArchivo.Enabled)
            {
                guardarArchivo.Enabled = true;
                guardarArchivo.PerformLayout();
            }

        }
   

        private void NuevoArchivo_Click(object sender, EventArgs e)
        {
            
            Nuevo_Archivo("");
            ventanaGuardada = false;
            VentanaEstaGuardada();
        }


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


        private Boolean AbrirArchivoEn(string direccion)
        {
            StreamReader archivo;
            int posicion, intChar, nivel;
            bool leyendo,estadoCheck;
            char caracter,opcion;
            string leido;
            Boolean respuesta;
            //abrir

            int i;
            
           
            

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
                    Pestañas.PerformLayout();
                    i = Pestañas.SelectedIndex;

                    if (Archivos.Count > 0)
                    {
                        if (Archivos[i].Guardado == 0)
                        {
                            Archivos[i].Guardado = 1;
                            guardarArchivo.Enabled = false;
                            guardarArchivo.PerformLayout();
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
                                            break;
                                        case '1':
                                            if (leyendo)
                                            {
                                                AgregarBoton(estadoCheck, leido);
                                            }
                                            leido = "";
                                            estadoCheck = true;
                                            leyendo = true;



                                            break;
                                        case '0':
                                            if (leyendo)
                                            {
                                                AgregarBoton(estadoCheck, leido);
                                            }
                                            leido = "";
                                            estadoCheck = false;
                                            leyendo = true;
                                            break;
                                        case 'P':
                                            if (leyendo)
                                            {
                                                AgregarBoton(estadoCheck, leido);
                                            }
                                            leido = "";
                                            estadoCheck = false;
                                            leyendo = false;
                                            break;
                                        case 'E':
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

        private void AbrirArchivo_Click(object sender, EventArgs e)
        {
            DialogResult resultado;
            resultado = abrirArchivoDialog.ShowDialog();
            
            
            if(resultado==DialogResult.OK) 
                if(AbrirArchivoEn(abrirArchivoDialog.FileName))
                {
                    ventanaGuardada = false;
                    VentanaEstaGuardada();
                }

        }



        private int GuardarArchivoEn(string direccion)
        {
            StreamWriter archivo;
            




            int auxiliar = 0;
            int i;


           



                i = this.Pestañas.SelectedIndex;

                 

                auxiliar = Archivos[i].Lista.Count;


                


                
                    archivo = new StreamWriter(direccion);


                    if (archivo != null)
                    {

                        archivo.WriteLine("#-B-#");


                        if(auxiliar>0) foreach (Nodo ITEM in Archivos[i].Lista)
                        {


                            {
                                TextBox txxt = ITEM.Objeto.Controls.OfType<TextBox>().FirstOrDefault();
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
                            guardarArchivo.Enabled = false;
                            guardarArchivo.PerformLayout();
                            VentanaEstaGuardada();

                            return 1;
                            
                        }

                    }
                

            
            return 0;
        }

        private void GuardarArchivo_Click(object sender, EventArgs e)
        {
            int i;
            i = Pestañas.SelectedIndex;
            if(Archivos.Count>0)
            {
                if (Archivos[i].Direccion != "" && Archivos[i].Direccion != "none")
                    GuardarArchivoEn(Archivos[i].Direccion);
                else GuardarArchivoComo();
            }


        }
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
                    Archivos[i].Pestaña.PerformLayout();
                    return 1;
                }
            
            return 0;
        }

        private void GuardarArchivoComo_Click(object sender, EventArgs e)
        {
            GuardarArchivoComo();
        }


        private void CerrarArchivoSinPreguntar()
        {
            int i;
            i = Pestañas.SelectedIndex;



            Archivos[i].Pestaña.Dispose();
            Archivos.RemoveAt(i);
            Pestañas.PerformLayout();
        }



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
                    Pestañas.PerformLayout();
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
                                Pestañas.PerformLayout();
                            }
                        }
                        else
                        {
                            if (GuardarArchivoComo() == 1)
                            {

                                Archivos[i].Pestaña.Dispose();
                                Archivos.RemoveAt(i);
                                Pestañas.PerformLayout();
                            }
                        }

                        respuesta = true;
                    }
                    else if (resultado == DialogResult.No)
                    {
                        Archivos[i].Pestaña.Dispose();
                        Archivos.RemoveAt(i);
                        Pestañas.PerformLayout();
                        respuesta = true;
                    }



                }
            }

            return respuesta;

        }
        private void CerrarArchivo_Click(object sender, EventArgs e)
        {
            if(PreguntarYCerrarArchivo())
            {
                ventanaGuardada = false;
                VentanaEstaGuardada();
            }
            
        }



        private void MostrarOcultarMenuVentana() 
        {
            if (!panelVentana.Visible)
            {
                panelVentana.Visible = true;
                Ventana.BackColor = SystemColors.Highlight;
            }
            else
            {
                panelVentana.Visible = false;
                Ventana.BackColor = SystemColors.Control;
            }

        }



        private void Ventana_Click(object sender, EventArgs e)
        {
            MostrarOcultarMenuVentana();
        
        }


        private void Panel2_Scroll(object sender, ScrollEventArgs e)
        {
  
            panelVentana.Top = 192 - panel2.VerticalScroll.Value;
        }
        private void CambioNodo(object sender, EventArgs e)
        {
            int i;
            i = Pestañas.SelectedIndex;
            if (Archivos[i].Guardado==1)
            { 
            Archivos[i].Guardado = 0;
            guardarArchivo.Enabled = true;
            guardarArchivo.PerformLayout();
   
            }
        }


        private void Pestañas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i;
            i = Pestañas.SelectedIndex;
            if (Archivos[i].Guardado == 0)
            {
                guardarArchivo.Enabled = true;
                guardarArchivo.PerformLayout();
                
            }
            else
            {
                guardarArchivo.Enabled = false;
                guardarArchivo.PerformLayout();
            }
        }



        private void About_Click(object sender, EventArgs e)
        {
            string cadena;

            cadena = "AS: ♦Andy's Software♦  \r\n\r\n\r\nAndy ToDo List Checker versión 1.0\r\n==================================\r\n\r\n2024 Lite version\r\n\r\n\r\nHecho por Andrés Hugo Bernárdez\r\n\r\n6 de enero del 2024 \r\n \r\nContacto: andreshugobernardez@gmail.com\r\n ";




            MessageBox.Show(cadena /*"Hecho por Andrés Hugo Bernárdez\n 2024\n\n andreshugobernardez@gmail.com"*/,"Andy ToDo List Checker 1.0");
        }



        List<string> ListaDeArchivos = new List<string>();

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
                Pestañas.PerformLayout();
                if (Archivos[i].Direccion == "" || Archivos[i].Direccion == "none")
                {

                    resultado = MessageBox.Show("¿Desea guardar el archivo \"" + Archivos[i].Nombre + "\" para guardarlo a la Ventana?", "Guardando Archivos en Ventana:", MessageBoxButtons.YesNoCancel);

                    if (resultado == DialogResult.Yes)
                    {

                        GuardarArchivoComo();
                        ListaDeArchivos.Add(Archivos[i].Direccion);
                       

                    }
                    if (resultado == DialogResult.No)
                    {
                        
                    }
                    if(resultado==DialogResult.Cancel)
                    {
                        Archivos[i].Status = -1;
                    }



                }
                else
                {
                    ListaDeArchivos.Add(Archivos[i].Direccion);
                    
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



        private Boolean GuardarVentanaComoEn()
        {
            DialogResult resultado;
            Boolean returned;

            
            returned = false;
            if (ListaDeArchivos.Count > 0)
            {



                resultado = guardarVentanaDialog.ShowDialog();

                if(resultado==DialogResult.OK)
                {
                    direccionVentana = guardarVentanaDialog.FileName;
                    returned=GuardarVentanaEn(direccionVentana);
                    

                }
                





            }
            else MessageBox.Show("No se puede guardar Ventana vacía", "Recordatorio:");

            return returned;
        }
        private Boolean GuardarVentanaEn(string direccion)
        {
            StreamWriter archivo;
            int i,k;
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
                        this.PerformLayout();

                        returned = true;

                    }
                }
            }
            else MessageBox.Show("No se puede guardar Ventana vacía", "Recordatorio:");


            return returned;
        }
        private void GuardarVentana_Click(object sender, EventArgs e)
        {
            if (!ventanaGuardada)
            {
                MostrarOcultarMenuVentana();
                LlenarListaDeArchivos();
                if (direccionVentana == "none" || direccionVentana == "")
                    GuardarVentanaComoEn();

                else GuardarVentanaEn(direccionVentana);
                    
            }
            
        }

        private void GuardarVentanaComo_Click(object sender, EventArgs e)
        {
            MostrarOcultarMenuVentana();
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



        private Boolean CerrarArchivosGuardados()
        {
            int i, n;
            Boolean retorno;
            n = Archivos.Count;
            retorno = true;
            for(i=n-1;i>-1;i--)
            {
                if (Archivos[i].Status != -1)
                {
                    CerrarArchivoSinPreguntar();
                }
                else
                {
                    Archivos[i].Status = 1;
                }
            }
            if(Archivos.Count>0)
            {
                ventanaGuardada = false;
                VentanaEstaGuardada();
                retorno = false;
            }
            return retorno;
        }


        private Boolean PreguntarYCerrarVentana()
        {
            DialogResult respuesta;
            
            Boolean returned;

            returned = false;
           
                if (ventanaGuardada)
                {
                    LlenarListaDeArchivos();
                    CerrarArchivosGuardados();
                this.Text = programaNombre;
                this.PerformLayout();
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
                            nombreVentana = "";
                            returned = true;
                            this.Text = programaNombre;
                            this.PerformLayout();
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



        private void CerrarVentana_Click(object sender, EventArgs e)
        {
            int i, n;

            MostrarOcultarMenuVentana();
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
                for (i=0;i<n;i++)
                {
                    ListaDeArchivos.RemoveAt(0);
                }
                nombreVentana = "";
                this.Text = programaNombre;
                this.PerformLayout();
            }

            
        }



        private Boolean AbrirVentanaEn()
        {
            DialogResult resultado;
            StreamReader archivo;
            string cadena;
            Boolean returned;

            returned = false;
            resultado = abrirVentanaDialog.ShowDialog();

            if(resultado==DialogResult.OK)
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
                            this.PerformLayout();
                        }

                        archivo.Close();
                    }

                }
            }
            return returned;
        }

        private void AbrirVentana_Click(object sender, EventArgs e)
        {
            MostrarOcultarMenuVentana();

            AbrirVentanaEn();


            

        }


    }

    /*
     Andrés Hugo Bernárdez 2024 andreshugobernárdez@gmail.com

     */
    
}
