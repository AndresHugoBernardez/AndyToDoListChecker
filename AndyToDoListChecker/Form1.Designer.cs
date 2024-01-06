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
            this.about = new System.Windows.Forms.Button();
            this.Ventana = new System.Windows.Forms.Button();
            this.abrirArchivo = new System.Windows.Forms.Button();
            this.cerrarArchivo = new System.Windows.Forms.Button();
            this.nuevoArchivo = new System.Windows.Forms.Button();
            this.guardarComoArchivo = new System.Windows.Forms.Button();
            this.guardarArchivo = new System.Windows.Forms.Button();
            this.Pestañas = new System.Windows.Forms.TabControl();
            this.panelVentana = new System.Windows.Forms.Panel();
            this.cerrarVentana = new System.Windows.Forms.Button();
            this.guardarComoVentana = new System.Windows.Forms.Button();
            this.guardarVentana = new System.Windows.Forms.Button();
            this.abrirVentana = new System.Windows.Forms.Button();
            this.abrirArchivoDialog = new System.Windows.Forms.OpenFileDialog();
            this.guardarArchivoDialog = new System.Windows.Forms.SaveFileDialog();
            this.guardarVentanaDialog = new System.Windows.Forms.SaveFileDialog();
            this.abrirVentanaDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel2.SuspendLayout();
            this.panelVentana.SuspendLayout();
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
            this.panel2.Controls.Add(this.about);
            this.panel2.Controls.Add(this.Ventana);
            this.panel2.Controls.Add(this.abrirArchivo);
            this.panel2.Controls.Add(this.cerrarArchivo);
            this.panel2.Controls.Add(this.nuevoArchivo);
            this.panel2.Controls.Add(this.nuevoItem);
            this.panel2.Controls.Add(this.guardarComoArchivo);
            this.panel2.Controls.Add(this.guardarArchivo);
            this.panel2.Location = new System.Drawing.Point(832, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(110, 610);
            this.panel2.TabIndex = 10;
            // 
            // about
            // 
            this.about.Location = new System.Drawing.Point(10, 278);
            this.about.Name = "about";
            this.about.Size = new System.Drawing.Size(75, 23);
            this.about.TabIndex = 8;
            this.about.Text = "Acerca de";
            this.about.UseVisualStyleBackColor = true;
            this.about.Click += new System.EventHandler(this.About_Click);
            // 
            // Ventana
            // 
            this.Ventana.BackColor = System.Drawing.SystemColors.Control;
            this.Ventana.Location = new System.Drawing.Point(10, 245);
            this.Ventana.Name = "Ventana";
            this.Ventana.Size = new System.Drawing.Size(75, 23);
            this.Ventana.TabIndex = 6;
            this.Ventana.Text = "Ventana";
            this.Ventana.UseVisualStyleBackColor = false;
            this.Ventana.Click += new System.EventHandler(this.Ventana_Click);
            // 
            // abrirArchivo
            // 
            this.abrirArchivo.Location = new System.Drawing.Point(10, 104);
            this.abrirArchivo.Name = "abrirArchivo";
            this.abrirArchivo.Size = new System.Drawing.Size(75, 23);
            this.abrirArchivo.TabIndex = 2;
            this.abrirArchivo.Text = "Abrir";
            this.abrirArchivo.UseVisualStyleBackColor = true;
            this.abrirArchivo.Click += new System.EventHandler(this.AbrirArchivo_Click);
            // 
            // cerrarArchivo
            // 
            this.cerrarArchivo.Location = new System.Drawing.Point(10, 200);
            this.cerrarArchivo.Name = "cerrarArchivo";
            this.cerrarArchivo.Size = new System.Drawing.Size(75, 23);
            this.cerrarArchivo.TabIndex = 5;
            this.cerrarArchivo.Text = "Cerrar";
            this.cerrarArchivo.UseVisualStyleBackColor = true;
            this.cerrarArchivo.Click += new System.EventHandler(this.CerrarArchivo_Click);
            // 
            // nuevoArchivo
            // 
            this.nuevoArchivo.Location = new System.Drawing.Point(10, 66);
            this.nuevoArchivo.Name = "nuevoArchivo";
            this.nuevoArchivo.Size = new System.Drawing.Size(75, 35);
            this.nuevoArchivo.TabIndex = 1;
            this.nuevoArchivo.Text = "Nuevo Archivo";
            this.nuevoArchivo.UseVisualStyleBackColor = true;
            this.nuevoArchivo.Click += new System.EventHandler(this.NuevoArchivo_Click);
            // 
            // guardarComoArchivo
            // 
            this.guardarComoArchivo.Location = new System.Drawing.Point(10, 158);
            this.guardarComoArchivo.Name = "guardarComoArchivo";
            this.guardarComoArchivo.Size = new System.Drawing.Size(75, 39);
            this.guardarComoArchivo.TabIndex = 4;
            this.guardarComoArchivo.TabStop = false;
            this.guardarComoArchivo.Text = "Guardar como...";
            this.guardarComoArchivo.UseVisualStyleBackColor = true;
            this.guardarComoArchivo.Click += new System.EventHandler(this.GuardarArchivoComo_Click);
            // 
            // guardarArchivo
            // 
            this.guardarArchivo.Enabled = false;
            this.guardarArchivo.Location = new System.Drawing.Point(10, 129);
            this.guardarArchivo.Name = "guardarArchivo";
            this.guardarArchivo.Size = new System.Drawing.Size(75, 28);
            this.guardarArchivo.TabIndex = 3;
            this.guardarArchivo.Text = "Guardar";
            this.guardarArchivo.UseVisualStyleBackColor = true;
            this.guardarArchivo.Click += new System.EventHandler(this.GuardarArchivo_Click);
            // 
            // Pestañas
            // 
            this.Pestañas.Location = new System.Drawing.Point(12, 10);
            this.Pestañas.Name = "Pestañas";
            this.Pestañas.SelectedIndex = 0;
            this.Pestañas.Size = new System.Drawing.Size(814, 610);
            this.Pestañas.TabIndex = 10;
            // 
            // panelVentana
            // 
            this.panelVentana.Controls.Add(this.cerrarVentana);
            this.panelVentana.Controls.Add(this.guardarComoVentana);
            this.panelVentana.Controls.Add(this.guardarVentana);
            this.panelVentana.Controls.Add(this.abrirVentana);
            this.panelVentana.Location = new System.Drawing.Point(746, 129);
            this.panelVentana.Name = "panelVentana";
            this.panelVentana.Size = new System.Drawing.Size(95, 149);
            this.panelVentana.TabIndex = 7;
            this.panelVentana.Visible = false;
            // 
            // cerrarVentana
            // 
            this.cerrarVentana.Location = new System.Drawing.Point(0, 117);
            this.cerrarVentana.Name = "cerrarVentana";
            this.cerrarVentana.Size = new System.Drawing.Size(95, 31);
            this.cerrarVentana.TabIndex = 3;
            this.cerrarVentana.Text = "Cerrar Ventana";
            this.cerrarVentana.UseVisualStyleBackColor = true;
            this.cerrarVentana.Click += new System.EventHandler(this.CerrarVentana_Click);
            // 
            // guardarComoVentana
            // 
            this.guardarComoVentana.Location = new System.Drawing.Point(0, 71);
            this.guardarComoVentana.Name = "guardarComoVentana";
            this.guardarComoVentana.Size = new System.Drawing.Size(95, 48);
            this.guardarComoVentana.TabIndex = 2;
            this.guardarComoVentana.Text = "Guardar Ventana Como...";
            this.guardarComoVentana.UseVisualStyleBackColor = true;
            this.guardarComoVentana.Click += new System.EventHandler(this.GuardarVentanaComo_Click);
            // 
            // guardarVentana
            // 
            this.guardarVentana.Enabled = false;
            this.guardarVentana.Location = new System.Drawing.Point(0, 34);
            this.guardarVentana.Name = "guardarVentana";
            this.guardarVentana.Size = new System.Drawing.Size(95, 39);
            this.guardarVentana.TabIndex = 1;
            this.guardarVentana.Text = "Guardar Ventana";
            this.guardarVentana.UseVisualStyleBackColor = true;
            this.guardarVentana.Click += new System.EventHandler(this.GuardarVentana_Click);
            // 
            // abrirVentana
            // 
            this.abrirVentana.Location = new System.Drawing.Point(0, 0);
            this.abrirVentana.Name = "abrirVentana";
            this.abrirVentana.Size = new System.Drawing.Size(95, 36);
            this.abrirVentana.TabIndex = 0;
            this.abrirVentana.Text = "Abrir Ventana";
            this.abrirVentana.UseVisualStyleBackColor = true;
            this.abrirVentana.Click += new System.EventHandler(this.AbrirVentana_Click);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 632);
            this.Controls.Add(this.panelVentana);
            this.Controls.Add(this.Pestañas);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Andy ToDo List Checker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            this.panelVentana.ResumeLayout(false);
            this.ResumeLayout(false);

        }

       



        #endregion
        private System.Windows.Forms.Button nuevoItem;
        private System.Windows.Forms.Panel panel2;
        private Button guardarArchivo;
        private TabControl Pestañas;
        private Button cerrarArchivo;
        private Button nuevoArchivo;
        private Button guardarComoArchivo;
        private Button abrirArchivo;
        private Button Ventana;
        private Panel panelVentana;
        private Button guardarComoVentana;
        private Button guardarVentana;
        private Button abrirVentana;
        private Button cerrarVentana;
        private OpenFileDialog abrirArchivoDialog;
        private Button about;
        private SaveFileDialog guardarArchivoDialog;
        private SaveFileDialog guardarVentanaDialog;
        private OpenFileDialog abrirVentanaDialog;
    }



}


