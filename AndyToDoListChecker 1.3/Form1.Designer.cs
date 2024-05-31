using System.Drawing;
using System.Windows.Forms;

namespace AndyToDoListChecker
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()

        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.nuevoItem = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CopiarBoton = new System.Windows.Forms.Button();
            this.PegarBoton = new System.Windows.Forms.Button();
            this.CortarBoton = new System.Windows.Forms.Button();
            this.EliminarItem = new System.Windows.Forms.Button();
            this.MoverAbajo = new System.Windows.Forms.Button();
            this.MoverArriba = new System.Windows.Forms.Button();
            this.MoverDerecha = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.MoverIzquierda = new System.Windows.Forms.Button();
            this.Pestañas = new System.Windows.Forms.TabControl();
            this.abrirArchivoDialog = new System.Windows.Forms.OpenFileDialog();
            this.guardarArchivoDialog = new System.Windows.Forms.SaveFileDialog();
            this.guardarVentanaDialog = new System.Windows.Forms.SaveFileDialog();
            this.abrirVentanaDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ediciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.agregarItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copiarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cortarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pegarItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.subirItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bajarItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.correrALaDerechaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.correrALaIzquierdaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.borrarItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ventanaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarComoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarVentanaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // nuevoItem
            // 
            this.nuevoItem.Location = new System.Drawing.Point(10, 12);
            this.nuevoItem.Name = "nuevoItem";
            this.nuevoItem.Size = new System.Drawing.Size(75, 35);
            this.nuevoItem.TabIndex = 0;
            this.nuevoItem.Text = "Nuevo Item";
            this.nuevoItem.UseVisualStyleBackColor = true;
            this.nuevoItem.Click += new System.EventHandler(this.NuevoItem_Click);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.CopiarBoton);
            this.panel2.Controls.Add(this.PegarBoton);
            this.panel2.Controls.Add(this.CortarBoton);
            this.panel2.Controls.Add(this.EliminarItem);
            this.panel2.Controls.Add(this.MoverAbajo);
            this.panel2.Controls.Add(this.MoverArriba);
            this.panel2.Controls.Add(this.MoverDerecha);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.MoverIzquierda);
            this.panel2.Controls.Add(this.nuevoItem);
            this.panel2.Location = new System.Drawing.Point(832, 34);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(110, 586);
            this.panel2.TabIndex = 10;
            this.panel2.TabStop = true;
            // 
            // CopiarBoton
            // 
            this.CopiarBoton.Location = new System.Drawing.Point(7, 180);
            this.CopiarBoton.Name = "CopiarBoton";
            this.CopiarBoton.Size = new System.Drawing.Size(78, 32);
            this.CopiarBoton.TabIndex = 11;
            this.CopiarBoton.Text = "Copiar Item";
            this.CopiarBoton.UseVisualStyleBackColor = true;
            this.CopiarBoton.Click += new System.EventHandler(this.CopiarBoton_Click);
            // 
            // PegarBoton
            // 
            this.PegarBoton.Location = new System.Drawing.Point(7, 256);
            this.PegarBoton.Name = "PegarBoton";
            this.PegarBoton.Size = new System.Drawing.Size(78, 32);
            this.PegarBoton.TabIndex = 11;
            this.PegarBoton.Text = "Pegar Item";
            this.PegarBoton.UseVisualStyleBackColor = true;
            this.PegarBoton.Click += new System.EventHandler(this.PegarBoton_Click);
            // 
            // CortarBoton
            // 
            this.CortarBoton.Location = new System.Drawing.Point(7, 218);
            this.CortarBoton.Name = "CortarBoton";
            this.CortarBoton.Size = new System.Drawing.Size(78, 32);
            this.CortarBoton.TabIndex = 11;
            this.CortarBoton.Text = "Cortar Item";
            this.CortarBoton.UseVisualStyleBackColor = true;
            this.CortarBoton.Click += new System.EventHandler(this.CortarBoton_Click);
            // 
            // EliminarItem
            // 
            this.EliminarItem.Location = new System.Drawing.Point(7, 294);
            this.EliminarItem.Name = "EliminarItem";
            this.EliminarItem.Size = new System.Drawing.Size(78, 34);
            this.EliminarItem.TabIndex = 10;
            this.EliminarItem.Text = "Eliminar Item";
            this.EliminarItem.UseVisualStyleBackColor = true;
            this.EliminarItem.Click += new System.EventHandler(this.EliminarItem_Click);
            // 
            // MoverAbajo
            // 
            this.MoverAbajo.Location = new System.Drawing.Point(36, 129);
            this.MoverAbajo.Name = "MoverAbajo";
            this.MoverAbajo.Size = new System.Drawing.Size(25, 27);
            this.MoverAbajo.TabIndex = 9;
            this.MoverAbajo.Text = "v";
            this.MoverAbajo.UseVisualStyleBackColor = true;
            this.MoverAbajo.Click += new System.EventHandler(this.MoverAbajo_Click);
            // 
            // MoverArriba
            // 
            this.MoverArriba.Location = new System.Drawing.Point(36, 63);
            this.MoverArriba.Name = "MoverArriba";
            this.MoverArriba.Size = new System.Drawing.Size(25, 27);
            this.MoverArriba.TabIndex = 9;
            this.MoverArriba.Text = "^";
            this.MoverArriba.UseVisualStyleBackColor = true;
            this.MoverArriba.Click += new System.EventHandler(this.MoverArriba_Click);
            // 
            // MoverDerecha
            // 
            this.MoverDerecha.Location = new System.Drawing.Point(60, 96);
            this.MoverDerecha.Name = "MoverDerecha";
            this.MoverDerecha.Size = new System.Drawing.Size(25, 27);
            this.MoverDerecha.TabIndex = 9;
            this.MoverDerecha.Text = ">";
            this.MoverDerecha.UseVisualStyleBackColor = true;
            this.MoverDerecha.Click += new System.EventHandler(this.MoverDerecha_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(60, 96);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(25, 27);
            this.button2.TabIndex = 9;
            this.button2.Text = "button1";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // MoverIzquierda
            // 
            this.MoverIzquierda.Location = new System.Drawing.Point(10, 96);
            this.MoverIzquierda.Name = "MoverIzquierda";
            this.MoverIzquierda.Size = new System.Drawing.Size(25, 27);
            this.MoverIzquierda.TabIndex = 9;
            this.MoverIzquierda.Text = "<";
            this.MoverIzquierda.UseVisualStyleBackColor = true;
            this.MoverIzquierda.Click += new System.EventHandler(this.MoverIzquierda_Click);
            // 
            // Pestañas
            // 
            this.Pestañas.AllowDrop = true;
            this.Pestañas.Location = new System.Drawing.Point(12, 34);
            this.Pestañas.Name = "Pestañas";
            this.Pestañas.SelectedIndex = 0;
            this.Pestañas.Size = new System.Drawing.Size(814, 586);
            this.Pestañas.TabIndex = 10;
            this.Pestañas.Click += new System.EventHandler(this.Pestañas_SelectedIndexChanged);
            this.Pestañas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseMove);
                
            // 
            // abrirArchivoDialog
            // 
            this.abrirArchivoDialog.Filter = "Archivo Andy To Do List (*.atd)|*.atd|Archivo de texto (*.txt)|*.txt";
            // 
            // guardarArchivoDialog
            // 
            this.guardarArchivoDialog.Filter = "Archivo Andy To Do List (*.atd)|*.atd|Archivo de texto (*.txt)|*.txt";
            // 
            // guardarVentanaDialog
            // 
            this.guardarVentanaDialog.Filter = "Archivo de Ventana de Andy(*.atdv)|*.atdv";
            // 
            // abrirVentanaDialog
            // 
            this.abrirVentanaDialog.Filter = "Archivo de Ventana de Andy(*.atdv)|*.atdv";
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.ediciónToolStripMenuItem,
            this.ventanaToolStripMenuItem,
            this.acercaDeToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(951, 24);
            this.menuStrip2.TabIndex = 11;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.abrirToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.guardarComoToolStripMenuItem,
            this.cerrarToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(205, 22);
            this.toolStripMenuItem1.Text = "Nuevo Archivo";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.NuevoArchivo_Click);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.abrirToolStripMenuItem.Text = "Abrir Archivo";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.guardarToolStripMenuItem.Text = "Guardar Archivo";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.guardarToolStripMenuItem_Click);
            // 
            // guardarComoToolStripMenuItem
            // 
            this.guardarComoToolStripMenuItem.Name = "guardarComoToolStripMenuItem";
            this.guardarComoToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.guardarComoToolStripMenuItem.Text = "Guardar Archivo Como...";
            this.guardarComoToolStripMenuItem.Click += new System.EventHandler(this.guardarComoToolStripMenuItem_Click);
            // 
            // cerrarToolStripMenuItem
            // 
            this.cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
            this.cerrarToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.cerrarToolStripMenuItem.Text = "Cerrar Archivo";
            this.cerrarToolStripMenuItem.Click += new System.EventHandler(this.cerrarToolStripMenuItem_Click);
            // 
            // ediciónToolStripMenuItem
            // 
            this.ediciónToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agregarItemToolStripMenuItem,
            this.copiarToolStripMenuItem,
            this.cortarToolStripMenuItem,
            this.pegarItemToolStripMenuItem,
            this.toolStripSeparator2,
            this.subirItemToolStripMenuItem,
            this.bajarItemToolStripMenuItem,
            this.correrALaDerechaToolStripMenuItem,
            this.correrALaIzquierdaToolStripMenuItem,
            this.toolStripSeparator1,
            this.borrarItemToolStripMenuItem});
            this.ediciónToolStripMenuItem.Name = "ediciónToolStripMenuItem";
            this.ediciónToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.ediciónToolStripMenuItem.Text = "Edición";
            // 
            // agregarItemToolStripMenuItem
            // 
            this.agregarItemToolStripMenuItem.Name = "agregarItemToolStripMenuItem";
            this.agregarItemToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.agregarItemToolStripMenuItem.Text = "Agregar Item";
            this.agregarItemToolStripMenuItem.Click += new System.EventHandler(this.agregarItemToolStripMenuItem_Click);
            // 
            // copiarToolStripMenuItem
            // 
            this.copiarToolStripMenuItem.Name = "copiarToolStripMenuItem";
            this.copiarToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.copiarToolStripMenuItem.Text = "Copiar Item";
            this.copiarToolStripMenuItem.Click += new System.EventHandler(this.copiarToolStripMenuItem_Click);
            // 
            // cortarToolStripMenuItem
            // 
            this.cortarToolStripMenuItem.Name = "cortarToolStripMenuItem";
            this.cortarToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.cortarToolStripMenuItem.Text = "Cortar Item";
            this.cortarToolStripMenuItem.Click += new System.EventHandler(this.cortarToolStripMenuItem_Click);
            // 
            // pegarItemToolStripMenuItem
            // 
            this.pegarItemToolStripMenuItem.Name = "pegarItemToolStripMenuItem";
            this.pegarItemToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.pegarItemToolStripMenuItem.Text = "Pegar Item";
            this.pegarItemToolStripMenuItem.Click += new System.EventHandler(this.pegarItemToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(176, 6);
            // 
            // subirItemToolStripMenuItem
            // 
            this.subirItemToolStripMenuItem.Name = "subirItemToolStripMenuItem";
            this.subirItemToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.subirItemToolStripMenuItem.Text = "Subir Item";
            this.subirItemToolStripMenuItem.Click += new System.EventHandler(this.subirItemToolStripMenuItem_Click);
            // 
            // bajarItemToolStripMenuItem
            // 
            this.bajarItemToolStripMenuItem.Name = "bajarItemToolStripMenuItem";
            this.bajarItemToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.bajarItemToolStripMenuItem.Text = "Bajar Item";
            this.bajarItemToolStripMenuItem.Click += new System.EventHandler(this.bajarItemToolStripMenuItem_Click);
            // 
            // correrALaDerechaToolStripMenuItem
            // 
            this.correrALaDerechaToolStripMenuItem.Name = "correrALaDerechaToolStripMenuItem";
            this.correrALaDerechaToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.correrALaDerechaToolStripMenuItem.Text = "Correr a la derecha";
            this.correrALaDerechaToolStripMenuItem.Click += new System.EventHandler(this.correrALaDerechaToolStripMenuItem_Click);
            // 
            // correrALaIzquierdaToolStripMenuItem
            // 
            this.correrALaIzquierdaToolStripMenuItem.Name = "correrALaIzquierdaToolStripMenuItem";
            this.correrALaIzquierdaToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.correrALaIzquierdaToolStripMenuItem.Text = "Correr a la izquierda";
            this.correrALaIzquierdaToolStripMenuItem.Click += new System.EventHandler(this.correrALaIzquierdaToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(176, 6);
            // 
            // borrarItemToolStripMenuItem
            // 
            this.borrarItemToolStripMenuItem.Name = "borrarItemToolStripMenuItem";
            this.borrarItemToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.borrarItemToolStripMenuItem.Text = "Borrar Item";
            this.borrarItemToolStripMenuItem.Click += new System.EventHandler(this.borrarItemToolStripMenuItem_Click);
            // 
            // ventanaToolStripMenuItem
            // 
            this.ventanaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem1,
            this.guardarToolStripMenuItem1,
            this.guardarComoToolStripMenuItem1,
            this.cerrarVentanaToolStripMenuItem});
            this.ventanaToolStripMenuItem.Name = "ventanaToolStripMenuItem";
            this.ventanaToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.ventanaToolStripMenuItem.Text = "Ventana";
            // 
            // abrirToolStripMenuItem1
            // 
            this.abrirToolStripMenuItem1.Name = "abrirToolStripMenuItem1";
            this.abrirToolStripMenuItem1.Size = new System.Drawing.Size(206, 22);
            this.abrirToolStripMenuItem1.Text = "Abrir Ventana";
            this.abrirToolStripMenuItem1.Click += new System.EventHandler(this.abrirToolStripMenuItem1_Click);
            // 
            // guardarToolStripMenuItem1
            // 
            this.guardarToolStripMenuItem1.Name = "guardarToolStripMenuItem1";
            this.guardarToolStripMenuItem1.Size = new System.Drawing.Size(206, 22);
            this.guardarToolStripMenuItem1.Text = "Guardar Ventana";
            this.guardarToolStripMenuItem1.Click += new System.EventHandler(this.guardarToolStripMenuItem1_Click);
            // 
            // guardarComoToolStripMenuItem1
            // 
            this.guardarComoToolStripMenuItem1.Name = "guardarComoToolStripMenuItem1";
            this.guardarComoToolStripMenuItem1.Size = new System.Drawing.Size(206, 22);
            this.guardarComoToolStripMenuItem1.Text = "Guardar Ventana Como...";
            this.guardarComoToolStripMenuItem1.Click += new System.EventHandler(this.guardarComoToolStripMenuItem1_Click);
            // 
            // cerrarVentanaToolStripMenuItem
            // 
            this.cerrarVentanaToolStripMenuItem.Name = "cerrarVentanaToolStripMenuItem";
            this.cerrarVentanaToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.cerrarVentanaToolStripMenuItem.Text = "Cerrar Ventana";
            this.cerrarVentanaToolStripMenuItem.Click += new System.EventHandler(this.cerrarVentanaToolStripMenuItem_Click);
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.acercaDeToolStripMenuItem.Text = "Acerca de...";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.acercaDeToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 632);
            this.Controls.Add(this.Pestañas);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip2;
            this.Name = "Form1";
            this.Text = "Andy ToDo List Checker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }





        #endregion
        private System.Windows.Forms.Button nuevoItem;
        private System.Windows.Forms.Panel panel2;
        private TabControl Pestañas;
        private OpenFileDialog abrirArchivoDialog;
        private SaveFileDialog guardarArchivoDialog;
        private SaveFileDialog guardarVentanaDialog;
        private OpenFileDialog abrirVentanaDialog;
     
        private ToolStripMenuItem toolStripMenuItem2;
        private MenuStrip menuStrip2;
        private ToolStripMenuItem archivoToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem abrirToolStripMenuItem;
        private ToolStripMenuItem guardarToolStripMenuItem;
        private ToolStripMenuItem guardarComoToolStripMenuItem;
        private ToolStripMenuItem ventanaToolStripMenuItem;
        private ToolStripMenuItem abrirToolStripMenuItem1;
        private ToolStripMenuItem guardarToolStripMenuItem1;
        private ToolStripMenuItem guardarComoToolStripMenuItem1;
        private ToolStripMenuItem cerrarVentanaToolStripMenuItem;
        private ToolStripMenuItem cerrarToolStripMenuItem;
        private ToolStripMenuItem ediciónToolStripMenuItem;
        private ToolStripMenuItem agregarItemToolStripMenuItem;
        private ToolStripMenuItem copiarToolStripMenuItem;
        private ToolStripMenuItem cortarToolStripMenuItem;
        private ToolStripMenuItem borrarItemToolStripMenuItem;
        private ToolStripMenuItem subirItemToolStripMenuItem;
        private ToolStripMenuItem bajarItemToolStripMenuItem;
        private ToolStripMenuItem correrALaDerechaToolStripMenuItem;
        private ToolStripMenuItem correrALaIzquierdaToolStripMenuItem;
        private ToolStripMenuItem pegarItemToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem acercaDeToolStripMenuItem;
        private Button EliminarItem;
        private Button MoverAbajo;
        private Button MoverArriba;
        private Button MoverDerecha;
        private Button button2;
        private Button MoverIzquierda;
        private Button CortarBoton;
        private Button CopiarBoton;
        private Button PegarBoton;
    }



}


