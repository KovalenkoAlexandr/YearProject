using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Shared;

namespace WorkingWithData
{
    public partial class FormRedaction : Form
    {
        InformationAboutDocumentAll[] masTypeDocument = new InformationAboutDocumentAll[50];
        int countMasTypeDocument = 0;
        int positionType = 0;

        InformationAboutDocumentAll[] masDisciplineDocument = new InformationAboutDocumentAll[10];
        int countMasDisciplineDocument = 0;
        int positionDiscipline = 0;

        InformationAboutDocumentAll[] masKeyWords = new InformationAboutDocumentAll[100];
        int countKeyWords = 0;
        int positionKeyWords = 0;

        InformationAboutDocumentAll[] masOldKeyWords = new InformationAboutDocumentAll[50];
        int countOldKeyWords = 0;
        int positionOldKeyWords = 0;



        public FormRedaction()
        {
            InitializeComponent();
        }

        private void FormRedaction_Load(object sender, EventArgs e)
        {
            if (MainForm.strWay[6] == "1") ToolStripMenuItem2.Visible = true;
            else ToolStripMenuItem2.Visible = false;

            toolStripStatusLabel6.Text += MainForm.strWay[1];

            toolStripStatusLabel1.Text += MainForm.strWay[0];
            toolStripStatusLabel2.Text += MainForm.strWay[3];
            toolStripStatusLabel4.Text += MainForm.strWay[4];
            toolStripStatusLabel3.Text += MainForm.strWay[2];

            createMasType();
            comboBox1.Text = MainForm.strWay[2];
            checkNewType();

            createMasDiscipline();
            comboBox2.Text = MainForm.strWay[1];
            checkNewDiscipline();

            createMasKeyWords();
            createMasOldKeywords();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkNewType();
            checkNewDiscipline();

            if (checkElementWithSameName() == true) MessageBox.Show("В одному файлі не може бути документів зі схожими назвами!", "Повідомлення");
            else
            {
                changeInformationAboutDocument();
                changeInformationAboutKeywords();

                Close();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                DialogResult resMessange = MessageBox.Show("Бажаєте додати до документу ключове слово - " + listBox1.SelectedItem.ToString() + "?", "Підтвердження дій", MessageBoxButtons.YesNo);

                if (resMessange == DialogResult.Yes)
                {
                    if (checkDocumentSimilarity(listBox1.SelectedItem.ToString()) == false)
                    {
                        MessageBox.Show("Ключове слово успішно додано!");

                        masOldKeyWords[countOldKeyWords] = new InformationAboutDocumentAll();

                        masOldKeyWords[countOldKeyWords].id = seekInOneElementInMasKeyWords(listBox1.SelectedItem.ToString());
                        masOldKeyWords[countOldKeyWords].NewElement = true;
                        masOldKeyWords[countOldKeyWords++].keyword = listBox1.SelectedItem.ToString();

                        listBox2.Items.Add(listBox1.SelectedItem.ToString());
                    }
                    else MessageBox.Show("Дане ключове слово вже є у списку ключових слів, які прив’язані к документу!", "Повідомлення");
                }
            }

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                DialogResult resMessange = MessageBox.Show("Бажаєте додати до документу ключове слово - " + listBox2.SelectedItem.ToString() + "?", "Підтвердження дій", MessageBoxButtons.YesNo);

                if (resMessange == DialogResult.Yes)
                {
                    if (deleteElementFromMasOldkeywords(listBox2.SelectedItem.ToString()) == true) MessageBox.Show("Ключове слово успішно видалено!");

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            deleteAllElementInListBox1();
            createMasKeyWords();

            seekInListBox1(textBox1.Text);
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult resMessange = MessageBox.Show("Ви дійсно бажаєте залишити форму?", "Підтвердження дій", MessageBoxButtons.YesNo);

            if (resMessange == DialogResult.Yes)
            {
                Close();
            }
        }



        void createMasType()
        {

            MySqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            string sql = "SELECT * FROM diploma.document_specification;";

            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                masTypeDocument[countMasTypeDocument] = new InformationAboutDocumentAll();

                masTypeDocument[countMasTypeDocument].id = Convert.ToInt32(reader[0]);
                masTypeDocument[countMasTypeDocument++].type = reader[1].ToString();

                comboBox1.Items.Add(reader[1].ToString());
            }
            reader.Close();
            conn.Close();
        }

        void checkNewType()
        {
            for (int i = 0; i < countMasTypeDocument; i++)
            {
                if (comboBox1.Text == masTypeDocument[i].type)
                {
                    positionType = i;
                    break;
                }
            }
        }

        void changeInformationAboutDocument()
        {

            MySqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            string sql = "UPDATE material SET ID_specification = " + masTypeDocument[positionType].id + ", ID_study_load = " + masDisciplineDocument[positionDiscipline].id + " WHERE ID_m = " + MainForm.strWay[5] + ";";

            MySqlCommand command = new MySqlCommand(sql, conn);

            command.ExecuteNonQuery();

            conn.Close();

            MessageBox.Show("Документ - " + MainForm.strWay[3] + " успішно змінено!");
        }

        void changeInformationAboutKeywords()
        {

            MySqlConnection conn = DBUtils.GetDBConnection();

            conn.Open();

            string sql = "DELETE FROM material_tag WHERE ID_material = " + MainForm.strWay[5] + ";";

            MySqlCommand command = new MySqlCommand(sql, conn);

            command.ExecuteNonQuery();

            conn.Close();

            conn.Open();

            for (int i = 0; i < countOldKeyWords; i++)
            {
                sql = "INSERT INTO material_tag(ID_material, ID_tag) VALUES('" + MainForm.strWay[5] + "', '" + masOldKeyWords[i].id + "');";

                command = new MySqlCommand(sql, conn);

                command.ExecuteNonQuery();
            }

            conn.Close();
        }

        void createMasDiscipline()
        {

            MySqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            string sql = "SELECT ID_s_l, ID_discipline, discipline_name FROM studyload, users, disciplines WHERE studyload.ID_user = users.ID_u and studyload.ID_discipline = disciplines.ID_d  and fio LIKE ('%" + MainForm.strWay[0] + "%');";

            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                masDisciplineDocument[countMasDisciplineDocument] = new InformationAboutDocumentAll();

                masDisciplineDocument[countMasDisciplineDocument].id = Convert.ToInt32(reader[0]);
                masDisciplineDocument[countMasDisciplineDocument++].discipline = reader[2].ToString();

                comboBox2.Items.Add(reader[2].ToString());
            }
            reader.Close();
            conn.Close();
        }

        void checkNewDiscipline()
        {
            for (int i = 0; i < countMasDisciplineDocument; i++)
            {
                if (comboBox2.Text == masDisciplineDocument[i].discipline)
                {
                    positionDiscipline = i;
                    break;
                }
            }
        }

        void createMasKeyWords()
        {

            MySqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            string sql = "SELECT * FROM diploma.tags;";

            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                masKeyWords[countKeyWords] = new InformationAboutDocumentAll();

                masKeyWords[countKeyWords].id = Convert.ToInt32(reader[0]);
                masKeyWords[countKeyWords++].keyword = reader[1].ToString();

                listBox1.Items.Add(reader[1].ToString());
            }
            reader.Close();
            conn.Close();

        }

        void seekInListBox1(string tmp)
        {
            int position = 0;
            int countElementInListBox = listBox1.Items.Count;

            for (int i = 0; i < countElementInListBox; i++)
            {
                if (relatedWordsFor_materials(listBox1.Items[position].ToString(), tmp) == false) listBox1.Items.RemoveAt(position);
                else position++;
            }
        }

        int seekInOneElementInMasKeyWords(string tmp)
        {

            for (int i = 0; i < countKeyWords; i++)
            {
                if (masKeyWords[i].keyword == tmp) return masKeyWords[i].id;
            }
            return 0;
        }

        void deleteAllElementInListBox1()
        {
            listBox1.Items.Clear();
            for (int i = 0; i < countKeyWords; i++) masKeyWords[i] = null;
            countKeyWords = 0;
        }

        bool relatedWordsFor_materials(string firstStr, string secondStr)
        {
            string tmpStr_1_1 = firstStr.ToLower();
            string tmpStr_2_1 = secondStr.ToLower();

            string[] tmpStr_1_3 = tmpStr_1_1.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string tmpStr_1_3_2 = "";
            foreach (string i in tmpStr_1_3)
            {
                tmpStr_1_3_2 += i;
            }
            string[] tmpStr_2_3 = tmpStr_2_1.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string tmpStr_2_3_2 = "";
            foreach (string i in tmpStr_2_3)
            {
                tmpStr_2_3_2 += i;
            }

            if (firstStr == secondStr) return true;
            if (tmpStr_1_1 == tmpStr_2_1) return true;
            if (tmpStr_1_3_2 == tmpStr_2_3_2) return true;
            if (tmpStr_1_1.IndexOf(tmpStr_2_1) != -1) return true;
            if (tmpStr_1_3_2.IndexOf(tmpStr_2_3_2) != -1) return true;

            return false;
        }

        void createMasOldKeywords()
        {

            MySqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            string sql = "SELECT ID_material, tags.ID_tag, Tag_name FROM material_tag, tags WHERE material_tag.ID_tag = tags.ID_tag and ID_material = " + MainForm.strWay[5] + ";";

            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                masOldKeyWords[countOldKeyWords] = new InformationAboutDocumentAll();

                masOldKeyWords[countOldKeyWords].id = Convert.ToInt32(reader[1]);
                masOldKeyWords[countOldKeyWords].NewElement = false;
                masOldKeyWords[countOldKeyWords++].keyword = reader[2].ToString();

                listBox2.Items.Add(reader[2].ToString());
            }
            reader.Close();
            conn.Close();
        }

        bool checkDocumentSimilarity(string tmp)
        {
            int countSimilarity = 0;
            for (int i = 0; i < countOldKeyWords; i++)
            {
                if (masOldKeyWords[i].keyword == tmp)
                {
                    countSimilarity++;
                    break;
                }
            }

            if (countSimilarity == 0) return false;
            else return true;
        }

        bool deleteElementFromMasOldkeywords(string tmp)
        {
            for (int i = 0; i < countOldKeyWords; i++)
            {
                if (masOldKeyWords[i].keyword == tmp)
                {
                    positionOldKeyWords = i;
                    break;
                }
            }

            if (listBox2.Items.Count > 1)
            {
                InformationAboutDocumentAll tmpObForSwapInformation = new InformationAboutDocumentAll();

                tmpObForSwapInformation.id = masOldKeyWords[positionOldKeyWords].id;
                tmpObForSwapInformation.keyword = masOldKeyWords[positionOldKeyWords].keyword;

                masOldKeyWords[positionOldKeyWords].id = masOldKeyWords[countOldKeyWords - 1].id;
                masOldKeyWords[positionOldKeyWords].keyword = masOldKeyWords[countOldKeyWords - 1].keyword;

                masOldKeyWords[countOldKeyWords - 1].id = tmpObForSwapInformation.id;
                masOldKeyWords[countOldKeyWords - 1].keyword = tmpObForSwapInformation.keyword;

                masOldKeyWords[countOldKeyWords - 1] = null;
                countOldKeyWords--;

                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    if (listBox2.Items[i].ToString() == tmp)
                    {
                        listBox2.Items.RemoveAt(i);
                        break;
                    }
                }

                return true;
            }
            else
            {
                MessageBox.Show("Документ має включати хоча б одне ключове слово!", "Повідомлення");
                return false;
            }


        }

        bool checkElementWithSameName()
        {

            MySqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            string sql = "SELECT ID_m, Name_doc_element FROM material WHERE ID_specification = " + masTypeDocument[positionType].id + " and ID_study_load = " + masDisciplineDocument[positionDiscipline].id + " and Name_doc_element = '" + MainForm.strWay[3] + "';";

            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (MainForm.strWay[5] != reader[0].ToString()) return true;
            }

            reader.Close();
            conn.Close();

            return false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
	}
}
