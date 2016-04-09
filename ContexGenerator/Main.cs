using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ContexGenerator
{
    public partial class Main : Form
    {
        #region Events

        public Main()
        {
            InitializeComponent();
        }

        private void izlazToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ddlServeri.SelectedIndex = 0;

            refreshData();
        }

        private void dohvatiPodatkeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refreshData();
        }

        private void bGeneriraj_Click(object sender, EventArgs e)
        {
            if (!chkSve.Checked && lbTablice.SelectedItem == null)
            {
                MessageBox.Show("Potrebno je odabrati barem jednu tablicu ili maknuti chk za sve", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GenerirajKlase();
            GenerirajModule();
            GenerirajFactorije();
            GenerirajRepozitorije();
            GenerirajHTML();
            GenerirajResurse();

            GenerirajResxFile();
            GenerirajResxDesignerFile();

            GenerirajMVCKontrolere();

            GenerirajAPIControllere();

            GeneriranjeZavrseno();
        }

        private void Main_ResizeEnd(object sender, EventArgs e)
        {
            splitContainer.SplitterDistance = 200;
        }

        private void btnResxTrazi_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;

            DialogResult r = folderBrowserDialog.ShowDialog();

            if (r == DialogResult.OK && folderBrowserDialog.SelectedPath != null)
            {
                txtResxPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnResxGeneriraj_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtResxPath.Text))
            {
                MessageBox.Show("Potrebno je odabrati odredišnu mapu", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!Directory.Exists(txtResxPath.Text))
            {
                MessageBox.Show("Putanja odredišne mape nije ispravna", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GenerirajResxFile(true);
            GenerirajResxDesignerFile(true);
            GeneriranjeZavrseno();
        }

        private void btnAngFacTrazi_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;

            DialogResult r = folderBrowserDialog.ShowDialog();

            if (r == DialogResult.OK && folderBrowserDialog.SelectedPath != null)
            {
                txtAngularFactory.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnFacGeneriraj_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtAngularFactory.Text))
            {
                MessageBox.Show("Potrebno je odabrati odredišnu mapu", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!Directory.Exists(txtAngularFactory.Text))
            {
                MessageBox.Show("Putanja odredišne mape nije ispravna", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GenerirajFactorije(true);
            GeneriranjeZavrseno();
        }

        private void btnAngModuleTrazi_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;

            DialogResult r = folderBrowserDialog.ShowDialog();

            if (r == DialogResult.OK && folderBrowserDialog.SelectedPath != null)
            {
                txtAngularModul.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnModGeneriraj_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtAngularModul.Text))
            {
                MessageBox.Show("Potrebno je odabrati odredišnu mapu", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!Directory.Exists(txtAngularModul.Text))
            {
                MessageBox.Show("Putanja odredišne mape nije ispravna", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GenerirajModule(true);
            GeneriranjeZavrseno();
        }

        private void btnHTMLTrazi_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;

            DialogResult r = folderBrowserDialog.ShowDialog();

            if (r == DialogResult.OK && folderBrowserDialog.SelectedPath != null)
            {
                txtHTML.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnGenerirajHTML_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtHTML.Text))
            {
                MessageBox.Show("Potrebno je odabrati odredišnu mapu", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!Directory.Exists(txtHTML.Text))
            {
                MessageBox.Show("Putanja odredišne mape nije ispravna", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GenerirajHTML(true);
            GeneriranjeZavrseno();
        }

        #endregion

        #region Metode

        /// <summary>
        /// Osvježi podatke (dohvaća tablice i storane procedure)
        /// </summary>
        private void refreshData()
        {
            DataTable dt = General.runSQL("SELECT * FROM INFORMATION_SCHEMA.TABLES t WHERE t.TABLE_NAME NOT IN ('__MigrationHistory','sysdiagrams')",
                                        txtServer.Text,
                                        txtBaza.Text,
                                        txtUserName.Text,
                                        txtPassword.Text
                                        );

            ListBox item;

            lbTablice.Items.Clear();

            // Dohvati nazive tablica
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ListBox()
                {
                    Text = dt.Rows[i]["TABLE_NAME"].ToString(),
                    Tag = dt.Rows[i]["TABLE_CATALOG"].ToString()
                };

                lbTablice.Items.Add(item);
                lbTablice.DisplayMember = "Text";
            }

            dt = General.runSQL("SELECT p.SPECIFIC_NAME FROM information_schema.parameters p WHERE p.specific_name NOT LIKE 'sp_%' AND p.specific_name NOT LIKE 'fn_%'",
                                        txtServer.Text,
                                        txtBaza.Text,
                                        txtUserName.Text,
                                        txtPassword.Text
                                        );

            lbProcedure.Items.Clear();

            // Dohvati nazive procedura
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ListBox()
                {
                    Text = dt.Rows[i]["SPECIFIC_NAME"].ToString()
                };

                lbProcedure.Items.Add(item);
                lbProcedure.DisplayMember = "Text";
            }
        }

        private void GeneriranjeZavrseno()
        {
            MessageBox.Show("Generiranje završeno", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Generiraj klase

        private void GenerirajKlase(bool zapisi = false)
        {
            // isprazni ritch text box
            rtbIspis.Clear();

            string display = "        [Display(Name = \"{0}\", ResourceType = typeof(Resources.{1}))]" + Environment.NewLine;
            string key = "        [KeyProperty]" + Environment.NewLine;
            string fKey = "        [KeyProperty]" + Environment.NewLine;
            string keyIdentity = "        [KeyProperty(Identity = true)]" + Environment.NewLine;
            string required = "        [Required]" + Environment.NewLine; // (ErrorMessageResourceType = typeof(Resources.{0}), ErrorMessageResourceName = \"{1}Required\")]" + Environment.NewLine;
            string lenght = "        [StringLength({0})]" + Environment.NewLine; //, ErrorMessageResourceType = typeof(Resources.{1}), ErrorMessageResourceName = \"{2}Long\")]" + Environment.NewLine;
            string multiline = "        [DataType(DataType.MultilineText)]" + Environment.NewLine;
            string jsonIgnore = "        [JsonIgnore]" + Environment.NewLine;

            // [Display(Name = "FirstName", ResourceType = typeof(Resources.Profile))]
            // [Required(ErrorMessageResourceType = typeof(Resources.Profile), ErrorMessageResourceName = "FirstNameRequired")]
            // [StringLength(100, ErrorMessageResourceType = typeof(Resources.Profile), ErrorMessageResourceName = "FirstNameLong")]

            string svojstva = String.Empty;
            string atributi = String.Empty;

            DataTable dt = new DataTable();

            for (int x = 0; x < lbTablice.Items.Count; x++)
            {
                if (!chkSve.Checked)
                {
                    if (lbTablice.SelectedIndex != x)
                    {
                        continue;
                    }
                }

                ListBox lb = (ListBox)lbTablice.Items[x];

                // Dohvati stupce i detalje o stupcima iz baze (za zadanu tablicu)
                //string SQL = String.Format("SELECT c.COLUMN_NAME, c.ORDINAL_POSITION, c.IS_NULLABLE, c.DATA_TYPE, c.CHARACTER_MAXIMUM_LENGTH " +
                //            "FROM INFORMATION_SCHEMA.COLUMNS c WHERE c.TABLE_CATALOG = '{0}' AND c.TABLE_NAME = '{1}'", lb.Tag, lb.Text);

                string SQL = String.Format("SELECT c.COLUMN_NAME, c.ORDINAL_POSITION, c.IS_NULLABLE, c.DATA_TYPE, CASE c.CHARACTER_MAXIMUM_LENGTH WHEN -1 THEN 2000 ELSE c.CHARACTER_MAXIMUM_LENGTH END as 'CHARACTER_MAXIMUM_LENGTH', ISNULL(o.CONSTRAINT_TYPE, 'None') as 'CONSTRAINT_TYPE', st.name, ISNULL(sc.name, 'NO') as 'Identity' " +
                                            "FROM INFORMATION_SCHEMA.COLUMNS c " +
                                            "LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE cu ON c.TABLE_CATALOG = cu.TABLE_CATALOG AND c.TABLE_NAME = cu.TABLE_NAME AND c.COLUMN_NAME = cu.COLUMN_NAME " +
                                            "LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS o ON c.TABLE_CATALOG = o.TABLE_CATALOG AND c.TABLE_NAME = o.TABLE_NAME AND o.CONSTRAINT_NAME = cu.CONSTRAINT_NAME " +
                                            "INNER JOIN sys.tables st ON c.TABLE_NAME = st.name " +
                                            "LEFT JOIN sys.identity_columns sc ON st.object_id = sc.object_id and c.COLUMN_NAME = sc.name " +
                                            "WHERE c.TABLE_CATALOG = '{0}' AND c.TABLE_NAME = '{1}'", lb.Tag, lb.Text);

                // dohvati podatke
                dt = General.runSQL(SQL,
                                    txtServer.Text,
                                    txtBaza.Text,
                                    txtUserName.Text,
                                    txtPassword.Text
                                    );

                svojstva = String.Empty;

                // za svaki stupac, složi kod
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    atributi = String.Format(display, dt.Rows[i]["COLUMN_NAME"], lb.Text);

                    if (dt.Rows[i]["CONSTRAINT_TYPE"].ToString() == "PRIMARY KEY" && dt.Rows[i]["Identity"].ToString() == "ID")
                    {
                        atributi = atributi + keyIdentity;
                    }
                    else if (dt.Rows[i]["CONSTRAINT_TYPE"].ToString() == "PRIMARY KEY")
                    {
                        atributi = atributi + key;
                    }
                    else if (dt.Rows[i]["CONSTRAINT_TYPE"].ToString() == "FOREIGN KEY" && (dt.Rows[i]["COLUMN_NAME"].ToString().Trim() == "UserID"))
                    {
                        atributi = atributi + fKey;
                    }

                    if (dt.Rows[i]["IS_NULLABLE"].ToString() == "NO" && 
                        ((dt.Rows[i]["COLUMN_NAME"].ToString().Trim() != "UserID") &&
                        (dt.Rows[i]["COLUMN_NAME"].ToString().Trim() != "ID")
                        ))
                    {
                        atributi = atributi + String.Format(required, lb.Text, dt.Rows[i]["COLUMN_NAME"]);
                    }

                    if (dt.Rows[i]["CHARACTER_MAXIMUM_LENGTH"].ToString() != "")
                    {
                        atributi = atributi + String.Format(lenght, dt.Rows[i]["CHARACTER_MAXIMUM_LENGTH"], lb.Text, dt.Rows[i]["COLUMN_NAME"]);

                        int maximum;

                        if (int.TryParse(dt.Rows[i]["CHARACTER_MAXIMUM_LENGTH"].ToString(), out maximum) && maximum > 30)
                        {
                            atributi = atributi + multiline;
                        }
                    }

                    if (dt.Rows[i]["COLUMN_NAME"].ToString().Trim() == "UserID")
                    {
                        atributi = atributi + jsonIgnore;
                    }

                    svojstva = svojstva + atributi;
                    svojstva = svojstva + String.Format(@"        public {0} {1} {{ get; set; }}{2}{3}",
                                                DBHelper.TipPodataka(dt.Rows[i]["DATA_TYPE"].ToString()),
                                                dt.Rows[i]["COLUMN_NAME"],
                                                i != dt.Rows.Count - 1 ? Environment.NewLine : "", // skraćeni if
                                                i != dt.Rows.Count - 1 ? Environment.NewLine : ""  // skraćeni if
                                                );
                }

                string klasa = String.Format(Predlosci.ClassTemplate,
                    lb.Text,
                    svojstva
                    );

                // ispiši sve
                rtbIspis.AppendText(klasa);
                rtbIspis.AppendText(Environment.NewLine);
                rtbIspis.AppendText(Environment.NewLine);

                if (zapisi)
                {
                    File.WriteAllText(txtKlase.Text.Trim() + "\\" + String.Format("{0}.cs", lb.Text),
                        klasa,
                        Encoding.UTF8);
                }
            }

            obojajStvari(rtbIspis);
        }

        #endregion

        #region Generiraj module

        private void GenerirajModule(bool zapisi = false)
        {
            rtbAngModul.Clear();

            for (int x = 0; x < lbTablice.Items.Count; x++)
            {
                if (!chkSve.Checked)
                {
                    if (lbTablice.SelectedIndex != x)
                    {
                        continue;
                    }
                }

                ListBox lb = (ListBox)lbTablice.Items[x];

                // ispiši sve
                rtbAngModul.AppendText(String.Format(Predlosci.AngularJSModul,
                    lb.Text
                    ));
                rtbAngModul.AppendText(Environment.NewLine);

                if (zapisi && lb.Text != "Profile")
                {
                    File.WriteAllText(txtAngularModul.Text.Trim() + "\\" + String.Format("{0}Module.js", lb.Text),
                        String.Format(Predlosci.AngularJSModul, lb.Text, lb.Text),
                        Encoding.UTF8);
                }
            }
        }

        #endregion

        #region Generiraj factorije

        private void GenerirajFactorije(bool zapisi = false)
        {
            rtbAngFactory.Clear();

            for (int x = 0; x < lbTablice.Items.Count; x++)
            {
                if (!chkSve.Checked)
                {
                    if (lbTablice.SelectedIndex != x)
                    {
                        continue;
                    }
                }

                ListBox lb = (ListBox)lbTablice.Items[x];

                // ispiši sve
                rtbAngFactory.AppendText(String.Format(Predlosci.AngularJSFactory,
                    lb.Text
                    ));
                rtbAngFactory.AppendText(Environment.NewLine);

                if (zapisi && lb.Text != "Profile")
                {
                    File.WriteAllText(txtAngularFactory.Text.Trim() + "\\" + String.Format("{0}Factory.js", lb.Text),
                        String.Format(Predlosci.AngularJSFactory, lb.Text, lb.Text),
                        Encoding.UTF8);
                }
            }
        }

        #endregion

        #region Generiraj repozitorije

        private void GenerirajRepozitorije(bool zapisi = false)
        {
            rtbRepository.Clear();

            for (int x = 0; x < lbTablice.Items.Count; x++)
            {
                if (!chkSve.Checked)
                {
                    if (lbTablice.SelectedIndex != x)
                    {
                        continue;
                    }
                }

                ListBox lb = (ListBox)lbTablice.Items[x];

                //// ispiši sve
                //string irepo = (String.Format(Predlosci.IRepositoryTemplate,
                //     lb.Text
                //     ));
                //rtbRepository.AppendText(irepo);
                //rtbRepository.AppendText(Environment.NewLine);

                //if (zapisi)
                //{
                //    File.WriteAllText(txtRepository.Text.Trim() + "\\" + String.Format("{0}.cs", "I" + lb.Text + "Repository"),
                //        irepo,
                //        Encoding.UTF8);
                //}

                // ispiši sve
                string repo = (String.Format(Predlosci.RepositoryTemplate, lb.Text, lb.Text.ToLower()));

                rtbRepository.AppendText(repo);
                rtbRepository.AppendText(Environment.NewLine);

                if (zapisi)
                {
                    File.WriteAllText(txtRepository.Text.Trim() + "\\" + String.Format("{0}.cs", lb.Text + "Repository"),
                        repo,
                        Encoding.UTF8);
                }
            }


            obojajStvari(rtbRepository);
        }

        #endregion

        #region GenerirajHTML

        private void GenerirajHTML(bool zapisi = false)
        {
            rtbHTML.Clear();

            string innerText = String.Empty;
            DataTable dt = null;

            bool required = false;
            bool lenght = false;

            for (int x = 0; x < lbTablice.Items.Count; x++)
            {
                if (!chkSve.Checked)
                {
                    if (lbTablice.SelectedIndex != x)
                    {
                        continue;
                    }
                }

                ListBox lb = (ListBox)lbTablice.Items[x];

                // Dohvati stupce i detalje o stupcima iz baze (za zadanu tablicu)
                string SQL = String.Format("SELECT c.COLUMN_NAME, c.ORDINAL_POSITION, c.IS_NULLABLE, c.DATA_TYPE, c.CHARACTER_MAXIMUM_LENGTH " +
                            "FROM INFORMATION_SCHEMA.COLUMNS c WHERE c.TABLE_CATALOG = '{0}' AND c.TABLE_NAME = '{1}'", lb.Tag, lb.Text);

                // dohvati podatke
                dt = General.runSQL(SQL,
                                    txtServer.Text,
                                    txtBaza.Text,
                                    txtUserName.Text,
                                    txtPassword.Text
                                    );

                // za svaki stupac, složi kod
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["COLUMN_NAME"].ToString() == "ID" || dt.Rows[i]["COLUMN_NAME"].ToString() == "UserID")
                    {
                        continue;
                    }
                    else
                    {
                        required = (dt.Rows[i]["IS_NULLABLE"].ToString() == "NO");
                        lenght = (DBHelper.TipPodataka(dt.Rows[i]["DATA_TYPE"].ToString()) == "string");

                        if (required && lenght)
                        {
                            innerText += String.Format(
                                            Predlosci.HTMLSingleTemplateRequiredLenght,
                                            lb.Text,
                                            dt.Rows[i]["COLUMN_NAME"],
                                            DBHelper.TipPodataka(dt.Rows[i]["DATA_TYPE"].ToString()));
                        }
                        else if (!required && lenght)
                        {
                            innerText += String.Format(
                                            Predlosci.HTMLSingleTemplateLenght,
                                            lb.Text,
                                            dt.Rows[i]["COLUMN_NAME"],
                                            DBHelper.TipPodataka(dt.Rows[i]["DATA_TYPE"].ToString()));
                        }
                        else if (required && !lenght)
                        {
                            innerText += String.Format(
                                            Predlosci.HTMLSingleTemplateRequired,
                                            lb.Text,
                                            dt.Rows[i]["COLUMN_NAME"],
                                            DBHelper.TipPodataka(dt.Rows[i]["DATA_TYPE"].ToString()));
                        }
                        else
                        {
                            innerText += String.Format(
                                            Predlosci.HTMLSingleTemplate,
                                            lb.Text,
                                            dt.Rows[i]["COLUMN_NAME"],
                                            DBHelper.TipPodataka(dt.Rows[i]["DATA_TYPE"].ToString()));
                        }
                    }
                }

                string text = String.Format(Predlosci.HTMLTemplate, lb.Text, innerText);
                text = text.Replace("XXX", "{{").Replace("YYY", "}}");

                if (zapisi && lb.Text != "Profile")
                {
                    if (!Directory.Exists(String.Format(txtHTML.Text.Trim() + "\\{0}", lb.Text)))
                    {
                        Directory.CreateDirectory(String.Format(txtHTML.Text.Trim() + "\\{0}", lb.Text));
                    }

                    File.WriteAllText(txtHTML.Text.Trim() + String.Format("\\{0}\\", lb.Text) + "Index.cshtml",
                        text,
                        Encoding.UTF8);
                }

                // ispiši sve
                rtbHTML.AppendText(text);
                rtbHTML.AppendText(Environment.NewLine);
            }

            obojajStvari(rtbHTML);
        }

        #endregion

        #region DohvatiNazivePropertya

        private void GenerirajResurse()
        {
            // isprazni ritch text box
            dgResursi.DataSource = null;

            if (chkSve.Checked)
            {
                return;
            }

            DataTable dt = new DataTable();

            for (int x = 0; x < lbTablice.Items.Count; x++)
            {
                if (!chkSve.Checked)
                {
                    if (lbTablice.SelectedIndex != x)
                    {
                        continue;
                    }
                }

                ListBox lb = (ListBox)lbTablice.Items[x];

                // Dohvati stupce i detalje o stupcima iz baze (za zadanu tablicu)
                string SQL = String.Format("SELECT c.COLUMN_NAME, c.ORDINAL_POSITION, c.IS_NULLABLE, c.DATA_TYPE, c.CHARACTER_MAXIMUM_LENGTH " +
                            "FROM INFORMATION_SCHEMA.COLUMNS c WHERE c.TABLE_CATALOG = '{0}' AND c.TABLE_NAME = '{1}'", lb.Tag, lb.Text);

                // dohvati podatke
                dt = General.runSQL(SQL,
                                    txtServer.Text,
                                    txtBaza.Text,
                                    txtUserName.Text,
                                    txtPassword.Text
                                    );

                dgResursi.DataSource = dt;
            }
        }

        #endregion

        #region GenerirajResxe

        private void GenerirajResxFile(bool zapisi = false)
        {
            rtbResx.Clear();

            string innerText = String.Empty;
            DataTable dt = null;

            for (int x = 0; x < lbTablice.Items.Count; x++)
            {
                if (!chkSve.Checked)
                {
                    if (lbTablice.SelectedIndex != x)
                    {
                        continue;
                    }
                }

                ListBox lb = (ListBox)lbTablice.Items[x];

                // Dohvati stupce i detalje o stupcima iz baze (za zadanu tablicu)
                string SQL = String.Format("SELECT c.COLUMN_NAME, c.ORDINAL_POSITION, c.IS_NULLABLE, c.DATA_TYPE, c.CHARACTER_MAXIMUM_LENGTH " +
                            "FROM INFORMATION_SCHEMA.COLUMNS c WHERE c.TABLE_CATALOG = '{0}' AND c.TABLE_NAME = '{1}'", lb.Tag, lb.Text);

                // dohvati podatke
                dt = General.runSQL(SQL,
                                    txtServer.Text,
                                    txtBaza.Text,
                                    txtUserName.Text,
                                    txtPassword.Text
                                    );

                string items = String.Empty;

                // za svaki stupac, složi kod
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    items += String.Format(Predlosci.SingleRresxItem, dt.Rows[i]["COLUMN_NAME"].ToString(), "");

                    if ((dt.Rows[i]["IS_NULLABLE"].ToString() == "NO"))
                    {
                        items += String.Format(Predlosci.SingleRresxItem, dt.Rows[i]["COLUMN_NAME"].ToString() + "Required", "is required");
                    }

                    if (DBHelper.TipPodataka(dt.Rows[i]["DATA_TYPE"].ToString()) == "string")
                    {
                        items += String.Format(Predlosci.SingleRresxItem, dt.Rows[i]["COLUMN_NAME"].ToString() + "Long", "has invalid lenght");
                    }
                }

                if (zapisi)
                {
                    File.WriteAllText(txtResxPath.Text.Trim() + "\\" + String.Format("{0}.resx", lb.Text),
                        String.Format(Predlosci.ResxFileTemplate, items, lb.Text),
                        Encoding.UTF8);
                }

                rtbResx.AppendText(String.Format(Predlosci.ResxFileTemplate, items, lb.Text) + Environment.NewLine);
                rtbResx.AppendText(Environment.NewLine);
            }
        }

        private void GenerirajResxDesignerFile(bool zapisi = false)
        {
            string namesp = "BikeGround.Models.Resources";

            rtbResxDesigner.Clear();

            string innerText = String.Empty;
            DataTable dt = null;

            for (int x = 0; x < lbTablice.Items.Count; x++)
            {
                if (!chkSve.Checked)
                {
                    if (lbTablice.SelectedIndex != x)
                    {
                        continue;
                    }
                }

                ListBox lb = (ListBox)lbTablice.Items[x];

                // Dohvati stupce i detalje o stupcima iz baze (za zadanu tablicu)
                string SQL = String.Format("SELECT c.COLUMN_NAME, c.ORDINAL_POSITION, c.IS_NULLABLE, c.DATA_TYPE, c.CHARACTER_MAXIMUM_LENGTH " +
                            "FROM INFORMATION_SCHEMA.COLUMNS c WHERE c.TABLE_CATALOG = '{0}' AND c.TABLE_NAME = '{1}'", lb.Tag, lb.Text);

                // dohvati podatke
                dt = General.runSQL(SQL,
                                    txtServer.Text,
                                    txtBaza.Text,
                                    txtUserName.Text,
                                    txtPassword.Text
                                    );

                string items = String.Empty;

                // za svaki stupac, složi kod
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    items += String.Format(Predlosci.SingleDesignerItem, dt.Rows[i]["COLUMN_NAME"].ToString(), "");
                    items += String.Format(Predlosci.SingleDesignerItem, dt.Rows[i]["COLUMN_NAME"].ToString() + "Required", "is required");

                    if (DBHelper.TipPodataka(dt.Rows[i]["DATA_TYPE"].ToString()) == "string")
                    {
                        items += String.Format(Predlosci.SingleDesignerItem, dt.Rows[i]["COLUMN_NAME"].ToString() + "Long", "has invalid lenght");
                    }
                }

                if (zapisi)
                {
                    File.WriteAllText(txtResxPath.Text.Trim() + "\\" + String.Format("{0}.Designer.cs", lb.Text),
                        String.Format(Predlosci.ResxDesignerFileTemplate, items, lb.Text, namesp),
                        Encoding.UTF8);
                }

                rtbResxDesigner.AppendText(String.Format(Predlosci.ResxDesignerFileTemplate, items, lb.Text, namesp) + Environment.NewLine);
                rtbResxDesigner.AppendText(Environment.NewLine);
            }
        }

        #endregion

        #region Generiraj API Controllere

        private void GenerirajAPIControllere(bool zapisi = false)
        {
            rtbAPIController.Clear();

            for (int x = 0; x < lbTablice.Items.Count; x++)
            {
                if (!chkSve.Checked)
                {
                    if (lbTablice.SelectedIndex != x)
                    {
                        continue;
                    }
                }

                ListBox lb = (ListBox)lbTablice.Items[x];

                // ispiši sve
                rtbAPIController.AppendText(String.Format(Predlosci.APIController,
                    lb.Text,
                    lb.Text.ToLower()
                    ));
                rtbAPIController.AppendText(Environment.NewLine);

                if (zapisi)// && lb.Text != "Profile")
                {
                    File.WriteAllText(txtAPIController.Text.Trim() + "\\" + String.Format("{0}Controller.cs", lb.Text),
                        String.Format(Predlosci.APIController, lb.Text, lb.Text.ToLower()),
                        Encoding.UTF8);
                }
            }

            obojajStvari(rtbAPIController);
        }

        #endregion

        private void obojajStvari(RichTextBox t)
        {
            General.primjeniRegex(Color.Green, t, RegexUzorci.zeleno);
            General.primjeniRegex(Color.Blue, t, RegexUzorci.plavo);
            General.primjeniRegex(Color.IndianRed, t, RegexUzorci.crveno);
        }

        private void btnMVCCOdaberi_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;

            DialogResult r = folderBrowserDialog.ShowDialog();

            if (r == DialogResult.OK && folderBrowserDialog.SelectedPath != null)
            {
                txtMvcControlers.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnMVCCKreiraj_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMvcControlers.Text))
            {
                MessageBox.Show("Potrebno je odabrati odredišnu mapu", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!Directory.Exists(txtMvcControlers.Text))
            {
                MessageBox.Show("Putanja odredišne mape nije ispravna", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GenerirajMVCKontrolere(true);
            GeneriranjeZavrseno();
        }

        private void GenerirajMVCKontrolere(bool zapisi = false)
        {
            rtbMVCControlers.Clear();

            for (int x = 0; x < lbTablice.Items.Count; x++)
            {
                if (!chkSve.Checked)
                {
                    if (lbTablice.SelectedIndex != x)
                    {
                        continue;
                    }
                }

                ListBox lb = (ListBox)lbTablice.Items[x];

                // ispiši sve
                rtbMVCControlers.AppendText(String.Format(Predlosci.MVCControler,
                    lb.Text
                    ));
                rtbMVCControlers.AppendText(Environment.NewLine);

                if (zapisi)
                {
                    File.WriteAllText(txtMvcControlers.Text.Trim() + "\\" + String.Format("{0}Controller.cs", lb.Text),
                        String.Format(Predlosci.MVCControler, lb.Text),
                        Encoding.UTF8);
                }
            }

            obojajStvari(rtbMVCControlers);
        }

        private void btnOdaberiKlase_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;

            DialogResult r = folderBrowserDialog.ShowDialog();

            if (r == DialogResult.OK && folderBrowserDialog.SelectedPath != null)
            {
                txtKlase.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnGenerirajKlase_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtKlase.Text))
            {
                MessageBox.Show("Potrebno je odabrati odredišnu mapu", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!Directory.Exists(txtKlase.Text))
            {
                MessageBox.Show("Putanja odredišne mape nije ispravna", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GenerirajKlase(true);
            GeneriranjeZavrseno();
        }

        private void btnRepository_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;

            DialogResult r = folderBrowserDialog.ShowDialog();

            if (r == DialogResult.OK && folderBrowserDialog.SelectedPath != null)
            {
                txtRepository.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnGenerirajRepository_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtRepository.Text))
            {
                MessageBox.Show("Potrebno je odabrati odredišnu mapu", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!Directory.Exists(txtRepository.Text))
            {
                MessageBox.Show("Putanja odredišne mape nije ispravna", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GenerirajRepozitorije(true);
            GeneriranjeZavrseno();
        }

        private void ddlServeri_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlServeri.SelectedIndex == 0)
            {
                txtServer.Text = "CADIZ";
                txtUserName.Text = "sa";
                txtBaza.Text = "BikeGround";
                txtPassword.Text = "romana";

                Putanje.logedUser = "petar";
            }
            else
            {
                txtServer.Text = "PC-PERISA2";
                txtUserName.Text = "BikeGround";
                txtBaza.Text = "BikeGround";
                txtPassword.Text = "perisa";

                Putanje.logedUser = "goran";
            }

            txtKlase.Text = Putanje.pathKlase;
            txtAngularModul.Text = Putanje.pathModul;
            txtAngularFactory.Text = Putanje.pathFactory;
            txtRepository.Text = Putanje.pathRepository;
            txtResxPath.Text = Putanje.pathResources;
            txtMvcControlers.Text = Putanje.pathMVCController;
            txtAPIController.Text = Putanje.pathAPIController;
        }

        private void btnOdaberiAPI_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;

            DialogResult r = folderBrowserDialog.ShowDialog();

            if (r == DialogResult.OK && folderBrowserDialog.SelectedPath != null)
            {
                txtAPIController.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnGenerirajAPI_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtAPIController.Text))
            {
                MessageBox.Show("Potrebno je odabrati odredišnu mapu", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!Directory.Exists(txtAPIController.Text))
            {
                MessageBox.Show("Putanja odredišne mape nije ispravna", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GenerirajAPIControllere(true);
            GeneriranjeZavrseno();
        }
    }
}