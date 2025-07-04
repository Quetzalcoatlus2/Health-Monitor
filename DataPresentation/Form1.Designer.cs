namespace DataPresentation
{
    partial class DataPresentation : System.Windows.Forms.Form // Clasa parțială pentru formularul principal, derivată din System.Windows.Forms.Form
    {
        /// <summary>
        /// Variabilă necesară pentru designer-ul Windows Forms.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Curăță resursele utilizate.
        /// </summary>
        /// <param name="disposing">True dacă resursele gestionate trebuie eliberate; altfel, False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) // Verifică dacă trebuie eliberate resursele
            {
                components.Dispose(); // Eliberează resursele gestionate
            }
            base.Dispose(disposing); // Apelează metoda de bază pentru eliberarea resurselor
        }

        #region Cod generat de Windows Form Designer

        /// <summary>
        /// Metodă necesară pentru suportul designer-ului. Nu modificați
        /// conținutul acestei metode cu editorul de cod.
        /// </summary>
        private void InitializeComponent()
        {
            // Inițializare container pentru componente
            components = new System.ComponentModel.Container();
            Label label1; // Etichetă pentru codul pacientului
                          // Legare la sursa de date pentru obiectele de tip SensorBase
            sensorBaseBindingSource = new BindingSource(components);
            // Combobox pentru selectarea codului pacientului la pornirea monitorizării
            cbPatientCodeStart = new ComboBox();
            // Buton pentru pornirea monitorizării
            bStartPumping = new Button();
            // Buton pentru oprirea monitorizării
            bStopPumping = new Button();
            // A doua sursă de binding pentru obiectele de tip SensorBase
            sensorBaseBindingSource1 = new BindingSource(components);
            // DataGridView pentru afișarea datelor senzorilor
            dgSensorBaseList = new DataGridView();
            // Coloană pentru afișarea codului pacientului
            patientCodeDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            // Coloană pentru afișarea tipului senzorului
            sensorTypeDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            // Coloană pentru afișarea valorii senzorului
            valueDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            // Coloană pentru afișarea timestamp-ului
            timestampDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            // TextBox pentru setarea perioadei timer-ului
            tbTimerPeriod = new TextBox();
            // Etichetă pentru descrierea TextBox-ului de perioadă
            label2 = new Label();
            // Etichetă suplimentară pentru descrierea perioadei între eșantioane
            label3 = new Label();
            // RichTextBox pentru afișarea alarmelor
            richTextBox1 = new RichTextBox();
            // Etichetă pentru zona de alarme
            label4 = new Label();
            // Combobox pentru filtrarea datelor după pacient
            cbFilterPatient = new ComboBox();
            // Control pentru selectarea datei la filtrare
            dtpFilterDate = new DateTimePicker();
            // Buton pentru filtrarea și afișarea datelor selectate
            bFilter = new Button();
            // GroupBox pentru secțiunea de filtrare a datelor
            groupBoxFiltering = new GroupBox();
            // Buton pentru afișarea datelor primite
            bReceived = new Button();
            // GroupBox pentru secțiunea de administrare a datelor (start/stop, perioadă, etc.)
            groupBoxData = new GroupBox();
            // RadioButton pentru opțiunea de trimitere a datelor prin TCP/IP
            radioButton1 = new RadioButton();
            // RadioButton pentru opțiunea de utilizare a canalului local
            radioButton2 = new RadioButton();
            // TextBox pentru introducerea adresei IP a serverului
            textBox1 = new TextBox();
            // Etichetă pentru indicarea adresei IP a serverului
            label5 = new Label();
            // GroupBox pentru opțiunile de canal de comunicare
            groupBox1 = new GroupBox();
            // Inițializare sau reinițializare a componentelor de design
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)sensorBaseBindingSource).BeginInit(); // Începe inițializarea sursei de date
            ((System.ComponentModel.ISupportInitialize)sensorBaseBindingSource1).BeginInit(); // Începe inițializarea celei de-a doua surse de date
            ((System.ComponentModel.ISupportInitialize)dgSensorBaseList).BeginInit(); // Începe inițializarea DataGridView-ului
            groupBoxFiltering.SuspendLayout(); // Suspendă layout-ul pentru GroupBox-ul de filtrare
            groupBoxData.SuspendLayout(); // Suspendă layout-ul pentru GroupBox-ul de date
            groupBox1.SuspendLayout(); // Suspendă layout-ul pentru GroupBox-ul de comunicare
            SuspendLayout(); // Suspendă layout-ul general al formularului
                             // 
                             // Configurarea etichetei label1 (Patient Code)
                             // 
            label1.AutoSize = true; // Setează auto-dimensionarea textului
            label1.Location = new Point(6, 19); // Setează locația etichetei
            label1.Name = "label1"; // Nume unic pentru etichetă
            label1.Size = new Size(75, 15); // Dimensiunea etichetei
            label1.TabIndex = 6; // Indexul de tabulare
            label1.Text = "Patient Code"; // Textul afișat pe etichetă
            label1.Click += label1_Click; // Eveniment pentru click pe etichetă
                                          // 
                                          // Configurarea binding-ului pentru sensorBase
                                          // 
            sensorBaseBindingSource.DataSource = typeof(SensorBase); // Setează tipul de date pentru binding
                                                                     // 
                                                                     // Configurarea comboboxului pentru pacient la start
                                                                     // 
            cbPatientCodeStart.FormattingEnabled = true; // Permite selectarea dintr-o listă
            cbPatientCodeStart.Location = new Point(6, 35); // Setează locația combobox-ului
            cbPatientCodeStart.Name = "cbPatientCodeStart"; // Nume unic pentru combobox
            cbPatientCodeStart.Size = new Size(169, 23); // Dimensiunea combobox-ului
            cbPatientCodeStart.TabIndex = 1; // Indexul de tabulare
            cbPatientCodeStart.SelectedIndexChanged += cbPatientCodeStart_SelectedIndexChanged; // Eveniment pentru schimbarea selecției
                                                                                                // 
                                                                                                // Configurarea butonului pentru pornirea monitorizării
                                                                                                // 
            bStartPumping.Location = new Point(198, 15); // Setează locația butonului
            bStartPumping.Name = "bStartPumping"; // Nume unic pentru buton
            bStartPumping.Size = new Size(169, 43); // Dimensiunea butonului
            bStartPumping.TabIndex = 2; // Indexul de tabulare
            bStartPumping.Text = "Start Monitoring"; // Textul afișat pe buton
            bStartPumping.UseVisualStyleBackColor = true; // Utilizează stilul implicit al butonului
            bStartPumping.Click += bStartPumping_Click; // Eveniment pentru click pe buton
                                                        // 
                                                        // Configurarea butonului pentru oprirea monitorizării
                                                        // 
            bStopPumping.Location = new Point(196, 72); // Setează locația butonului
            bStopPumping.Name = "bStopPumping"; // Nume unic pentru buton
            bStopPumping.Size = new Size(169, 44); // Dimensiunea butonului
            bStopPumping.TabIndex = 3; // Indexul de tabulare
            bStopPumping.Text = "Stop Monitoring"; // Textul afișat pe buton
            bStopPumping.UseVisualStyleBackColor = true; // Utilizează stilul implicit al butonului
            bStopPumping.Click += bStopPumping_Click; // Eveniment pentru click pe buton
                                                      // 
                                                      // Configurarea celei de-a doua surse de binding pentru sensorBase
                                                      // 
            sensorBaseBindingSource1.DataSource = typeof(SensorBase); // Setează tipul de date pentru binding
                                                                      // 
                                                                      // Configurarea grid-ului pentru afișarea datelor senzorilor
                                                                      // 
            dgSensorBaseList.AutoGenerateColumns = false; // Dezactivează generarea automată a coloanelor
            dgSensorBaseList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Setează redimensionarea automată a coloanelor
            dgSensorBaseList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; // Setează redimensionarea automată a rândurilor
            dgSensorBaseList.BackgroundColor = SystemColors.ActiveCaptionText; // Setează culoarea de fundal
            dgSensorBaseList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize; // Setează modul de afișare a antetelor coloanelor
            dgSensorBaseList.Columns.AddRange(new DataGridViewColumn[] { patientCodeDataGridViewTextBoxColumn1, sensorTypeDataGridViewTextBoxColumn1, valueDataGridViewTextBoxColumn1, timestampDataGridViewTextBoxColumn1 }); // Adaugă coloanele în grid
            dgSensorBaseList.DataSource = sensorBaseBindingSource; // Setează sursa de date
            dgSensorBaseList.GridColor = SystemColors.MenuHighlight; // Setează culoarea grilei
            dgSensorBaseList.Location = new Point(2, 2); // Setează locația grid-ului
            dgSensorBaseList.Name = "dgSensorBaseList"; // Nume unic pentru grid
            dgSensorBaseList.Size = new Size(472, 618); // Dimensiunea grid-ului
            dgSensorBaseList.StandardTab = true; // Permite navigarea cu tab-ul
            dgSensorBaseList.TabIndex = 4; // Indexul de tabulare
            dgSensorBaseList.CellContentClick += dgSensorBaseList_CellContentClick; // Eveniment pentru click pe celulele grid-ului
                                                                                    // 
                                                                                    // Configurarea coloanei pentru codul pacientului în grid
                                                                                    // 
            patientCodeDataGridViewTextBoxColumn1.DataPropertyName = "PatientCode"; // Setează proprietatea de legare la date
            patientCodeDataGridViewTextBoxColumn1.HeaderText = "Patient"; // Textul afișat în antetul coloanei
            patientCodeDataGridViewTextBoxColumn1.Name = "patientCodeDataGridViewTextBoxColumn1"; // Nume unic pentru coloană
            patientCodeDataGridViewTextBoxColumn1.ReadOnly = true; // Setează coloana ca fiind doar pentru citire
            patientCodeDataGridViewTextBoxColumn1.Width = 69; // Lățimea coloanei
                                                              // 
                                                              // Configurarea coloanei pentru tipul senzorului în grid
                                                              // 
            sensorTypeDataGridViewTextBoxColumn1.DataPropertyName = "SensorType"; // Setează proprietatea de legare la date
            sensorTypeDataGridViewTextBoxColumn1.HeaderText = "Sensor"; // Textul afișat în antetul coloanei
            sensorTypeDataGridViewTextBoxColumn1.Name = "sensorTypeDataGridViewTextBoxColumn1"; // Nume unic pentru coloană
            sensorTypeDataGridViewTextBoxColumn1.ReadOnly = true; // Setează coloana ca fiind doar pentru citire
            sensorTypeDataGridViewTextBoxColumn1.Width = 67; // Lățimea coloanei
                                                             // 
                                                             // Configurarea coloanei pentru valoarea senzorului în grid
                                                             // 
            valueDataGridViewTextBoxColumn1.DataPropertyName = "Value"; // Setează proprietatea de legare la date
            valueDataGridViewTextBoxColumn1.HeaderText = "Value"; // Textul afișat în antetul coloanei
            valueDataGridViewTextBoxColumn1.Name = "valueDataGridViewTextBoxColumn1"; // Nume unic pentru coloană
            valueDataGridViewTextBoxColumn1.ReadOnly = true; // Setează coloana ca fiind doar pentru citire
            valueDataGridViewTextBoxColumn1.Width = 60; // Lățimea coloanei
                                                        // 
                                                        // Configurarea coloanei pentru timestamp în grid
                                                        // 
            timestampDataGridViewTextBoxColumn1.DataPropertyName = "Timestamp"; // Setează proprietatea de legare la date
            timestampDataGridViewTextBoxColumn1.HeaderText = "Timestamp"; // Textul afișat în antetul coloanei
            timestampDataGridViewTextBoxColumn1.Name = "timestampDataGridViewTextBoxColumn1"; // Nume unic pentru coloană
            timestampDataGridViewTextBoxColumn1.ReadOnly = true; // Setează coloana ca fiind doar pentru citire
            timestampDataGridViewTextBoxColumn1.Width = 92; // Lățimea coloanei
                                                            // 
                                                            // Configurarea TextBox-ului pentru perioada timer-ului
                                                            // 
            tbTimerPeriod.Location = new Point(6, 93); // Setează locația TextBox-ului
            tbTimerPeriod.Name = "tbTimerPeriod"; // Nume unic pentru TextBox
            tbTimerPeriod.Size = new Size(169, 23); // Dimensiunea TextBox-ului
            tbTimerPeriod.TabIndex = 5; // Indexul de tabulare
            tbTimerPeriod.TextChanged += tbTimerPeriod_TextChanged; // Eveniment pentru schimbarea textului
                                                                    // 
                                                                    // Configurarea etichetei pentru perioada de timp
                                                                    // 
            label2.AutoSize = true; // Setează auto-dimensionarea textului
            label2.Location = new Point(7, 75); // Setează locația etichetei
            label2.Name = "label2"; // Nume unic pentru etichetă
            label2.Size = new Size(74, 15); // Dimensiunea etichetei
            label2.TabIndex = 7; // Indexul de tabulare
            label2.Text = "Time period "; // Textul afișat pe etichetă
            label2.Click += label2_Click; // Eveniment pentru click pe etichetă
                                          // 
                                          // Configurarea etichetei suplimentare pentru perioada între eșantioane
                                          // 
            label3.AutoSize = true; // Setează auto-dimensionarea textului
            label3.Location = new Point(73, 75); // Setează locația etichetei
            label3.Name = "label3"; // Nume unic pentru etichetă
            label3.Size = new Size(98, 15); // Dimensiunea etichetei
            label3.TabIndex = 8; // Indexul de tabulare
            label3.Text = "between samples"; // Textul afișat pe etichetă
            label3.Click += label3_Click; // Eveniment pentru click pe etichetă
                                          // 
                                          // Configurarea RichTextBox-ului pentru afișarea alarmelor
                                          // 
            richTextBox1.Location = new Point(480, 163); // Setează locația RichTextBox-ului
            richTextBox1.Name = "richTextBox1"; // Nume unic pentru RichTextBox
            richTextBox1.ScrollBars = RichTextBoxScrollBars.Vertical; // Activează scroll-ul vertical
            richTextBox1.Size = new Size(378, 132); // Dimensiunea RichTextBox-ului
            richTextBox1.TabIndex = 9; // Indexul de tabulare
            richTextBox1.Text = ""; // Textul implicit
                                    // 
                                    // Configurarea etichetei pentru zona de alarme
                                    // 
            label4.AutoSize = true; // Setează auto-dimensionarea textului
            label4.Location = new Point(641, 145); // Setează locația etichetei
            label4.Name = "label4"; // Nume unic pentru etichetă
            label4.Size = new Size(44, 15); // Dimensiunea etichetei
            label4.TabIndex = 10; // Indexul de tabulare
            label4.Text = "Alarms"; // Textul afișat pe etichetă
                                    // 
                                    // Configurarea combobox-ului pentru filtrarea după pacient
                                    // 
            cbFilterPatient.FormattingEnabled = true; // Permite selectarea dintr-o listă
            cbFilterPatient.Location = new Point(6, 22); // Setează locația combobox-ului
            cbFilterPatient.Name = "cbFilterPatient"; // Nume unic pentru combobox
            cbFilterPatient.Size = new Size(159, 23); // Dimensiunea combobox-ului
            cbFilterPatient.TabIndex = 11; // Indexul de tabulare
                                           // 
                                           // Configurarea DateTimePicker-ului pentru selecția datei la filtrare
                                           // 
            dtpFilterDate.Location = new Point(171, 22); // Setează locația DateTimePicker-ului
            dtpFilterDate.Name = "dtpFilterDate"; // Nume unic pentru DateTimePicker
            dtpFilterDate.Size = new Size(200, 23); // Dimensiunea DateTimePicker-ului
            dtpFilterDate.TabIndex = 12; // Indexul de tabulare
                                         // 
                                         // Configurarea butonului pentru filtrare și afișarea datelor selectate
                                         // 
            bFilter.Location = new Point(5, 51); // Setează locația butonului
            bFilter.Name = "bFilter"; // Nume unic pentru buton
            bFilter.Size = new Size(160, 46); // Dimensiunea butonului
            bFilter.TabIndex = 13; // Indexul de tabulare
            bFilter.Text = "Display Selected Data"; // Textul afișat pe buton
            bFilter.UseVisualStyleBackColor = true; // Utilizează stilul implicit al butonului
            bFilter.Click += bFilter_Click; // Eveniment pentru click pe buton
                                            // 
                                            // Configurarea GroupBox-ului pentru filtrare
                                            // 
            groupBoxFiltering.Controls.Add(bReceived); // Adaugă butonul pentru afișarea datelor primite
            groupBoxFiltering.Controls.Add(bFilter); // Adaugă butonul pentru filtrare
            groupBoxFiltering.Controls.Add(dtpFilterDate); // Adaugă DateTimePicker-ul pentru selecția datei
            groupBoxFiltering.Controls.Add(cbFilterPatient); // Adaugă combobox-ul pentru selectarea pacientului
            groupBoxFiltering.Location = new Point(479, 298); // Setează locația GroupBox-ului
            groupBoxFiltering.Name = "groupBoxFiltering"; // Nume unic pentru GroupBox
            groupBoxFiltering.Size = new Size(379, 156); // Dimensiunea GroupBox-ului
            groupBoxFiltering.TabIndex = 14; // Indexul de tabulare
            groupBoxFiltering.TabStop = false; // Dezactivează tab-ul pentru GroupBox
            groupBoxFiltering.Text = "Filtering"; // Textul afișat pe GroupBox
                                                  // 
                                                  // Configurarea butonului pentru afișarea datelor primite
                                                  // 
            bReceived.Location = new Point(6, 103); // Setează locația butonului
            bReceived.Name = "bReceived"; // Nume unic pentru buton
            bReceived.Size = new Size(159, 47); // Dimensiunea butonului
            bReceived.TabIndex = 14; // Indexul de tabulare
            bReceived.Text = "Display Received Data"; // Textul afișat pe buton
            bReceived.UseVisualStyleBackColor = true; // Utilizează stilul implicit al butonului
            bReceived.Click += bReceive_Click; // Eveniment pentru click pe buton
                                               // 
                                               // Configurarea GroupBox-ului pentru secțiunea de date (start/stop, perioadă, etc.)
                                               // 
            groupBoxData.Controls.Add(label3); // Adaugă eticheta suplimentară pentru perioada între eșantioane
            groupBoxData.Controls.Add(label2); // Adaugă eticheta pentru perioada de timp
            groupBoxData.Controls.Add(tbTimerPeriod); // Adaugă TextBox-ul pentru perioada timer-ului
            groupBoxData.Controls.Add(bStopPumping); // Adaugă butonul pentru oprirea monitorizării
            groupBoxData.Controls.Add(label1); // Adaugă eticheta pentru codul pacientului
            groupBoxData.Controls.Add(bStartPumping); // Adaugă butonul pentru pornirea monitorizării
            groupBoxData.Controls.Add(cbPatientCodeStart); // Adaugă combobox-ul pentru selectarea pacientului
            groupBoxData.Location = new Point(485, 11); // Setează locația GroupBox-ului
            groupBoxData.Name = "groupBoxData"; // Nume unic pentru GroupBox
            groupBoxData.Size = new Size(373, 131); // Dimensiunea GroupBox-ului
            groupBoxData.TabIndex = 15; // Indexul de tabulare
            groupBoxData.TabStop = false; // Dezactivează tab-ul pentru GroupBox
            groupBoxData.Text = "Data"; // Textul afișat pe GroupBox
                                        // 
                                        // Configurarea RadioButton-ului pentru trimiterea datelor prin TCP/IP
                                        // 
            radioButton1.AutoSize = true; // Setează auto-dimensionarea textului
            radioButton1.Location = new Point(4, 84); // Setează locația RadioButton-ului
            radioButton1.Name = "radioButton1"; // Nume unic pentru RadioButton
            radioButton1.Size = new Size(160, 19); // Dimensiunea RadioButton-ului
            radioButton1.TabIndex = 15; // Indexul de tabulare
            radioButton1.TabStop = true; // Setează RadioButton-ul ca fiind selectabil
            radioButton1.Text = "Send also through TCP/IP\r\n"; // Textul afișat pe RadioButton
            radioButton1.UseVisualStyleBackColor = true; // Utilizează stilul implicit al RadioButton-ului
            radioButton1.CheckedChanged += radioButton1_CheckedChanged; // Eveniment pentru schimbarea selecției
                                                                        // 
                                                                        // Configurarea RadioButton-ului pentru opțiunea locală
                                                                        // 
            radioButton2.AutoSize = true; // Setează auto-dimensionarea textului
            radioButton2.Location = new Point(5, 37); // Setează locația RadioButton-ului
            radioButton2.Name = "radioButton2"; // Nume unic pentru RadioButton
            radioButton2.Size = new Size(53, 19); // Dimensiunea RadioButton-ului
            radioButton2.TabIndex = 16; // Indexul de tabulare
            radioButton2.TabStop = true; // Setează RadioButton-ul ca fiind selectabil
            radioButton2.Text = "Local"; // Textul afișat pe RadioButton
            radioButton2.UseVisualStyleBackColor = true; // Utilizează stilul implicit al RadioButton-ului
            radioButton2.CheckedChanged += radioButton2_CheckedChanged; // Eveniment pentru schimbarea selecției
                                                                        // 
                                                                        // Configurarea TextBox-ului pentru introducerea adresei IP a serverului
                                                                        // 
            textBox1.Location = new Point(194, 80); // Setează locația TextBox-ului
            textBox1.Name = "textBox1"; // Nume unic pentru TextBox
            textBox1.Size = new Size(167, 23); // Dimensiunea TextBox-ului
            textBox1.TabIndex = 17; // Indexul de tabulare
            textBox1.Text = "192.168.0.105"; // Textul implicit afișat în TextBox
                                             // 
                                             // Configurarea etichetei pentru adresa IP a serverului
                                             // 
            label5.AutoSize = true; // Setează auto-dimensionarea textului
            label5.Location = new Point(243, 56); // Setează locația etichetei
            label5.Name = "label5"; // Nume unic pentru etichetă
            label5.Size = new Size(52, 15); // Dimensiunea etichetei
            label5.TabIndex = 18; // Indexul de tabulare
            label5.Text = "Server IP"; // Textul afișat pe etichetă
                                       // 
                                       // Configurarea GroupBox-ului pentru canalul de comunicare
                                       // 
            groupBox1.Controls.Add(radioButton1); // Adaugă RadioButton-ul pentru TCP/IP
            groupBox1.Controls.Add(label5); // Adaugă eticheta pentru adresa IP
            groupBox1.Controls.Add(radioButton2); // Adaugă RadioButton-ul pentru opțiunea locală
            groupBox1.Controls.Add(textBox1); // Adaugă TextBox-ul pentru adresa IP
            groupBox1.Location = new Point(480, 478); // Setează locația GroupBox-ului
            groupBox1.Name = "groupBox1"; // Nume unic pentru GroupBox
            groupBox1.Size = new Size(378, 128); // Dimensiunea GroupBox-ului
            groupBox1.TabIndex = 19; // Indexul de tabulare
            groupBox1.TabStop = false; // Dezactivează tab-ul pentru GroupBox
            groupBox1.Text = "Communication channel"; // Textul afișat pe GroupBox
            groupBox1.Enter += groupBox1_Enter; // Eveniment pentru intrarea în GroupBox
                                                // 
                                                // Configurarea principală a formularului DataPresentation
                                                // 
            BackColor = SystemColors.ActiveCaption; // Setează culoarea de fundal a formularului
            ClientSize = new Size(868, 618); // Setează dimensiunea formularului
            Controls.Add(groupBox1); // Adaugă GroupBox-ul pentru canalul de comunicare
            Controls.Add(groupBoxData); // Adaugă GroupBox-ul pentru secțiunea de date
            Controls.Add(groupBoxFiltering); // Adaugă GroupBox-ul pentru filtrare
            Controls.Add(label4); // Adaugă eticheta pentru zona de alarme
            Controls.Add(richTextBox1); // Adaugă RichTextBox-ul pentru afișarea alarmelor
            Controls.Add(dgSensorBaseList); // Adaugă DataGridView-ul pentru afișarea datelor senzorilor
            Name = "DataPresentation"; // Nume unic pentru formular
            ((System.ComponentModel.ISupportInitialize)sensorBaseBindingSource).EndInit(); // Finalizează inițializarea sursei de date
            ((System.ComponentModel.ISupportInitialize)sensorBaseBindingSource1).EndInit(); // Finalizează inițializarea celei de-a doua surse de date
            ((System.ComponentModel.ISupportInitialize)dgSensorBaseList).EndInit(); // Finalizează inițializarea DataGridView-ului
            groupBoxFiltering.ResumeLayout(false); // Reia layout-ul pentru GroupBox-ul de filtrare
            groupBoxData.ResumeLayout(false); // Reia layout-ul pentru GroupBox-ul de date
            groupBoxData.PerformLayout(); // Aplică layout-ul pentru controalele din GroupBox-ul de date
            groupBox1.ResumeLayout(false); // Reia layout-ul pentru GroupBox-ul de comunicare
            groupBox1.PerformLayout(); // Aplică layout-ul pentru controalele din GroupBox-ul de comunicare
            ResumeLayout(false); // Reia layout-ul general al formularului
            PerformLayout(); // Aplică layout-ul pentru toate controalele
                             // Cod suplimentar de inițializare a componentelor poate fi adăugat aici
        }
        #endregion
        private BindingSource sensorBaseBindingSource; // Sursă de binding pentru obiectele de tip SensorBase
        private ComboBox cbPatientCodeStart; // Combobox pentru selectarea codului pacientului
        private Button bStartPumping; // Buton pentru pornirea monitorizării
        private Button bStopPumping; // Buton pentru oprirea monitorizării
        private BindingSource sensorBaseBindingSource1; // A doua sursă de binding pentru obiectele de tip SensorBase
        private DataGridView dgSensorBaseList; // DataGridView pentru afișarea datelor senzorilor
        private DataGridViewTextBoxColumn patientCodeDataGridViewTextBoxColumn1; // Coloană pentru codul pacientului
        private DataGridViewTextBoxColumn sensorTypeDataGridViewTextBoxColumn1; // Coloană pentru tipul senzorului
        private DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn1; // Coloană pentru valoarea senzorului
        private DataGridViewTextBoxColumn timestampDataGridViewTextBoxColumn1; // Coloană pentru timestamp
        private TextBox tbTimerPeriod; // TextBox pentru setarea perioadei timer-ului
        private Label label2; // Etichetă pentru perioada de timp
        private Label label3; // Etichetă suplimentară pentru perioada între eșantioane
        private RichTextBox richTextBox1; // RichTextBox pentru afișarea alarmelor
        private Label label4; // Etichetă pentru zona de alarme
        private ComboBox cbFilterPatient; // Combobox pentru filtrarea datelor după pacient
        private DateTimePicker dtpFilterDate; // Control pentru selectarea datei la filtrare
        private Button bFilter; // Buton pentru filtrarea și afișarea datelor selectate
        private GroupBox groupBoxFiltering; // GroupBox pentru secțiunea de filtrare a datelor
        private GroupBox groupBoxData; // GroupBox pentru secțiunea de administrare a datelor
        private Button bReceived; // Buton pentru afișarea datelor primite
        private RadioButton radioButton1; // RadioButton pentru trimiterea datelor prin TCP/IP
        private RadioButton radioButton2; // RadioButton pentru opțiunea locală
        private TextBox textBox1; // TextBox pentru introducerea adresei IP a serverului
        private Label label5; // Etichetă pentru adresa IP a serverului
        private GroupBox groupBox1; // GroupBox pentru opțiunile de canal de comunicare
    }
}
