namespace DataPresentation
{
    partial class DataPresentation : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Cod generat de Windows Form Designer

        private void InitializeComponent()
        {

            components = new System.ComponentModel.Container();
            Label label1;

            sensorBaseBindingSource = new BindingSource(components);

            cbPatientCodeStart = new ComboBox();

            bStartPumping = new Button();

            bStopPumping = new Button();

            sensorBaseBindingSource1 = new BindingSource(components);

            dgSensorBaseList = new DataGridView();

            patientCodeDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();

            sensorTypeDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();

            valueDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();

            timestampDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();

            tbTimerPeriod = new TextBox();

            label2 = new Label();

            label3 = new Label();

            richTextBox1 = new RichTextBox();

            label4 = new Label();

            cbFilterPatient = new ComboBox();

            dtpFilterDate = new DateTimePicker();

            bFilter = new Button();

            groupBoxFiltering = new GroupBox();

            bReceived = new Button();

            groupBoxData = new GroupBox();

            radioButton1 = new RadioButton();

            radioButton2 = new RadioButton();

            textBox1 = new TextBox();

            label5 = new Label();

            groupBox1 = new GroupBox();

            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)sensorBaseBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sensorBaseBindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgSensorBaseList).BeginInit();
            groupBoxFiltering.SuspendLayout();
            groupBoxData.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();



            label1.AutoSize = true;
            label1.Location = new Point(6, 19);
            label1.Name = "label1";
            label1.Size = new Size(75, 15);
            label1.TabIndex = 6;
            label1.Text = "Patient Code";
            label1.Click += label1_Click;



            sensorBaseBindingSource.DataSource = typeof(SensorBase);



            cbPatientCodeStart.FormattingEnabled = true;
            cbPatientCodeStart.Location = new Point(6, 35);
            cbPatientCodeStart.Name = "cbPatientCodeStart";
            cbPatientCodeStart.Size = new Size(169, 23);
            cbPatientCodeStart.TabIndex = 1;
            cbPatientCodeStart.SelectedIndexChanged += cbPatientCodeStart_SelectedIndexChanged;



            bStartPumping.Location = new Point(198, 15);
            bStartPumping.Name = "bStartPumping";
            bStartPumping.Size = new Size(169, 43);
            bStartPumping.TabIndex = 2;
            bStartPumping.Text = "Start Monitoring";
            bStartPumping.UseVisualStyleBackColor = true;
            bStartPumping.Click += bStartPumping_Click;



            bStopPumping.Location = new Point(196, 72);
            bStopPumping.Name = "bStopPumping";
            bStopPumping.Size = new Size(169, 44);
            bStopPumping.TabIndex = 3;
            bStopPumping.Text = "Stop Monitoring";
            bStopPumping.UseVisualStyleBackColor = true;
            bStopPumping.Click += bStopPumping_Click;



            sensorBaseBindingSource1.DataSource = typeof(SensorBase);



            dgSensorBaseList.AutoGenerateColumns = false;
            dgSensorBaseList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgSensorBaseList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgSensorBaseList.BackgroundColor = SystemColors.ActiveCaptionText;
            dgSensorBaseList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgSensorBaseList.Columns.AddRange(new DataGridViewColumn[] { patientCodeDataGridViewTextBoxColumn1, sensorTypeDataGridViewTextBoxColumn1, valueDataGridViewTextBoxColumn1, timestampDataGridViewTextBoxColumn1 });
            dgSensorBaseList.DataSource = sensorBaseBindingSource;
            dgSensorBaseList.GridColor = SystemColors.MenuHighlight;
            dgSensorBaseList.Location = new Point(2, 2);
            dgSensorBaseList.Name = "dgSensorBaseList";
            dgSensorBaseList.Size = new Size(472, 618);
            dgSensorBaseList.StandardTab = true;
            dgSensorBaseList.TabIndex = 4;
            dgSensorBaseList.CellContentClick += dgSensorBaseList_CellContentClick;



            patientCodeDataGridViewTextBoxColumn1.DataPropertyName = "PatientCode";
            patientCodeDataGridViewTextBoxColumn1.HeaderText = "Patient";
            patientCodeDataGridViewTextBoxColumn1.Name = "patientCodeDataGridViewTextBoxColumn1";
            patientCodeDataGridViewTextBoxColumn1.ReadOnly = true;
            patientCodeDataGridViewTextBoxColumn1.Width = 69;



            sensorTypeDataGridViewTextBoxColumn1.DataPropertyName = "SensorType";
            sensorTypeDataGridViewTextBoxColumn1.HeaderText = "Sensor";
            sensorTypeDataGridViewTextBoxColumn1.Name = "sensorTypeDataGridViewTextBoxColumn1";
            sensorTypeDataGridViewTextBoxColumn1.ReadOnly = true;
            sensorTypeDataGridViewTextBoxColumn1.Width = 67;



            valueDataGridViewTextBoxColumn1.DataPropertyName = "Value";
            valueDataGridViewTextBoxColumn1.HeaderText = "Value";
            valueDataGridViewTextBoxColumn1.Name = "valueDataGridViewTextBoxColumn1";
            valueDataGridViewTextBoxColumn1.ReadOnly = true;
            valueDataGridViewTextBoxColumn1.Width = 60;



            timestampDataGridViewTextBoxColumn1.DataPropertyName = "Timestamp";
            timestampDataGridViewTextBoxColumn1.HeaderText = "Timestamp";
            timestampDataGridViewTextBoxColumn1.Name = "timestampDataGridViewTextBoxColumn1";
            timestampDataGridViewTextBoxColumn1.ReadOnly = true;
            timestampDataGridViewTextBoxColumn1.Width = 92;



            tbTimerPeriod.Location = new Point(6, 93);
            tbTimerPeriod.Name = "tbTimerPeriod";
            tbTimerPeriod.Size = new Size(169, 23);
            tbTimerPeriod.TabIndex = 5;
            tbTimerPeriod.TextChanged += tbTimerPeriod_TextChanged;



            label2.AutoSize = true;
            label2.Location = new Point(7, 75);
            label2.Name = "label2";
            label2.Size = new Size(74, 15);
            label2.TabIndex = 7;
            label2.Text = "Time period ";
            label2.Click += label2_Click;



            label3.AutoSize = true;
            label3.Location = new Point(73, 75);
            label3.Name = "label3";
            label3.Size = new Size(98, 15);
            label3.TabIndex = 8;
            label3.Text = "between samples";
            label3.Click += label3_Click;



            richTextBox1.Location = new Point(480, 163);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBox1.Size = new Size(378, 132);
            richTextBox1.TabIndex = 9;
            richTextBox1.Text = "";



            label4.AutoSize = true;
            label4.Location = new Point(641, 145);
            label4.Name = "label4";
            label4.Size = new Size(44, 15);
            label4.TabIndex = 10;
            label4.Text = "Alarms";



            cbFilterPatient.FormattingEnabled = true;
            cbFilterPatient.Location = new Point(6, 22);
            cbFilterPatient.Name = "cbFilterPatient";
            cbFilterPatient.Size = new Size(159, 23);
            cbFilterPatient.TabIndex = 11;



            dtpFilterDate.Location = new Point(171, 22);
            dtpFilterDate.Name = "dtpFilterDate";
            dtpFilterDate.Size = new Size(200, 23);
            dtpFilterDate.TabIndex = 12;



            bFilter.Location = new Point(5, 51);
            bFilter.Name = "bFilter";
            bFilter.Size = new Size(160, 46);
            bFilter.TabIndex = 13;
            bFilter.Text = "Display Selected Data";
            bFilter.UseVisualStyleBackColor = true;
            bFilter.Click += bFilter_Click;



            groupBoxFiltering.Controls.Add(bReceived);
            groupBoxFiltering.Controls.Add(bFilter);
            groupBoxFiltering.Controls.Add(dtpFilterDate);
            groupBoxFiltering.Controls.Add(cbFilterPatient);
            groupBoxFiltering.Location = new Point(479, 298);
            groupBoxFiltering.Name = "groupBoxFiltering";
            groupBoxFiltering.Size = new Size(379, 156);
            groupBoxFiltering.TabIndex = 14;
            groupBoxFiltering.TabStop = false;
            groupBoxFiltering.Text = "Filtering";



            bReceived.Location = new Point(6, 103);
            bReceived.Name = "bReceived";
            bReceived.Size = new Size(159, 47);
            bReceived.TabIndex = 14;
            bReceived.Text = "Display Received Data";
            bReceived.UseVisualStyleBackColor = true;
            bReceived.Click += bReceive_Click;



            groupBoxData.Controls.Add(label3);
            groupBoxData.Controls.Add(label2);
            groupBoxData.Controls.Add(tbTimerPeriod);
            groupBoxData.Controls.Add(bStopPumping);
            groupBoxData.Controls.Add(label1);
            groupBoxData.Controls.Add(bStartPumping);
            groupBoxData.Controls.Add(cbPatientCodeStart);
            groupBoxData.Location = new Point(485, 11);
            groupBoxData.Name = "groupBoxData";
            groupBoxData.Size = new Size(373, 131);
            groupBoxData.TabIndex = 15;
            groupBoxData.TabStop = false;
            groupBoxData.Text = "Data";



            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(4, 84);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(160, 19);
            radioButton1.TabIndex = 15;
            radioButton1.TabStop = true;
            radioButton1.Text = "Send also through TCP/IP\r\n";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;



            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(5, 37);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(53, 19);
            radioButton2.TabIndex = 16;
            radioButton2.TabStop = true;
            radioButton2.Text = "Local";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;



            textBox1.Location = new Point(194, 80);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(167, 23);
            textBox1.TabIndex = 17;
            textBox1.Text = "192.168.0.105";



            label5.AutoSize = true;
            label5.Location = new Point(243, 56);
            label5.Name = "label5";
            label5.Size = new Size(52, 15);
            label5.TabIndex = 18;
            label5.Text = "Server IP";



            groupBox1.Controls.Add(radioButton1);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Location = new Point(480, 478);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(378, 128);
            groupBox1.TabIndex = 19;
            groupBox1.TabStop = false;
            groupBox1.Text = "Communication channel";
            groupBox1.Enter += groupBox1_Enter;



            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(868, 618);
            Controls.Add(groupBox1);
            Controls.Add(groupBoxData);
            Controls.Add(groupBoxFiltering);
            Controls.Add(label4);
            Controls.Add(richTextBox1);
            Controls.Add(dgSensorBaseList);
            Name = "DataPresentation";
            ((System.ComponentModel.ISupportInitialize)sensorBaseBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)sensorBaseBindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgSensorBaseList).EndInit();
            groupBoxFiltering.ResumeLayout(false);
            groupBoxData.ResumeLayout(false);
            groupBoxData.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }
        #endregion
        private BindingSource sensorBaseBindingSource;
        private ComboBox cbPatientCodeStart;
        private Button bStartPumping;
        private Button bStopPumping;
        private BindingSource sensorBaseBindingSource1;
        private DataGridView dgSensorBaseList;
        private DataGridViewTextBoxColumn patientCodeDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn sensorTypeDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn timestampDataGridViewTextBoxColumn1;
        private TextBox tbTimerPeriod;
        private Label label2;
        private Label label3;
        private RichTextBox richTextBox1;
        private Label label4;
        private ComboBox cbFilterPatient;
        private DateTimePicker dtpFilterDate;
        private Button bFilter;
        private GroupBox groupBoxFiltering;
        private GroupBox groupBoxData;
        private Button bReceived;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private TextBox textBox1;
        private Label label5;
        private GroupBox groupBox1;
    }
}
