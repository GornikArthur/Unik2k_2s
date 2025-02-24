using System.Data.Common;
using System.Linq;
using System.Text;

namespace Lab1
{
	public partial class Form1 : Form
	{
		static string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
		private int alphabetSize = alphabet.Length;
		public Form1()
		{
			InitializeComponent();
			ColumnTextBox1.Enabled = false;
			ColumnTextBox2.Enabled = false;
			ColumnTextBox3.Enabled = false;
			ColumnButtonMake.Enabled = false;
			ColumnButtonGet.Enabled = false;
			ColumnCheckBox.Checked = true;

			VigenereTextBox1.Enabled = false;
			VigenereTextBox2.Enabled = false;
			VigenereButtonMake.Enabled = false;
			VigenereButtonGet.Enabled = false;

			SaveButton.Enabled = false;
		}
		private void ColumnCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (ColumnCheckBox.Checked)
			{
				ColumnTextBox1.Enabled = true;
				ColumnTextBox2.Enabled = true;
				ColumnTextBox3.Enabled = true;
				VigenereCheckBox.Checked = false;
			}
			else
			{
				ColumnTextBox1.Enabled = false;
				ColumnTextBox2.Enabled = false;
				ColumnTextBox3.Enabled = false;
				ColumnButtonMake.Enabled = false;
				ColumnButtonGet.Enabled = false;
			}
			ColumnButtonGetMakeEnable();
		}
		private void ColumnButtonGetMakeEnable()
		{
			if (ColumnCheckBox.Checked && ColumnTextBox1.Text.Length > 0 && ColumnTextBox2.Text.Length > 0 && ColumnTextBox3.Text.Length > 0)
			{
				ColumnButtonGet.Enabled = true;
				ColumnButtonMake.Enabled = true;
			}
			else
			{
				ColumnButtonGet.Enabled = false;
				ColumnButtonMake.Enabled = false;
			}
		}
		private void ColumnTextBox1_TextChanged_1(object sender, EventArgs e)
		{
			ColumnButtonGetMakeEnable();
		}
		private void ColumnTextBox2_TextChanged(object sender, EventArgs e)
		{
			ColumnButtonGetMakeEnable();
		}
		private void ColumnTextBox2_TextChanged_1(object sender, EventArgs e)
		{
			ColumnButtonGetMakeEnable();
		}
		private bool CheckTextBox(TextBox MyTextBox)
		{
			// Приводим текст к верхнему регистру и удаляем пробелы
			string newText = MyTextBox.Text.ToUpper().Replace(" ", "");

			// Фильтруем только символы, содержащиеся в alphabet
			newText = new string(newText.Where(c => alphabet.Contains(c)).ToArray());

			// Обновляем TextBox с отфильтрованным текстом
			MyTextBox.Text = newText;

			// Если текст не пустой, возвращаем true
			return newText.Length > 0;
		}
		private void ColumnButtonMake_Click(object sender, EventArgs e)
		{
			if (CheckTextBox(ColumnTextBox1) & CheckTextBox(ColumnTextBox2) & CheckTextBox(ColumnTextBox3))
			{
				ColumnShif columnShif = new ColumnShif(ColumnTextBox1.Text, ColumnTextBox2.Text, ColumnTextBox3.Text);
				ResTextBox.Text = columnShif.MakeShif();
			}
			else
			{
				ColumnButtonGetMakeEnable();
			}
		}
		private void ColumnButtonGet_Click(object sender, EventArgs e)
		{
			if (CheckTextBox(ColumnTextBox1) & CheckTextBox(ColumnTextBox2) & CheckTextBox(ColumnTextBox3))
			{
				ColumnShif columnShif = new ColumnShif(ColumnTextBox1.Text, ColumnTextBox2.Text, ColumnTextBox3.Text);
				ResTextBox.Text = columnShif.GetStart();
			}
			else
			{
				ColumnButtonGetMakeEnable();
			}
		}

		private void VigenereButtonGetMakeEnable()
		{
			if (VigenereCheckBox.Checked && VigenereTextBox1.Text.Length > 0 && VigenereTextBox2.Text.Length > 0)
			{
				VigenereButtonGet.Enabled = true;
				VigenereButtonMake.Enabled = true;
			}
			else
			{
				VigenereButtonGet.Enabled = false;
				VigenereButtonMake.Enabled = false;
			}
		}
		private void VigenereTextBox1_TextChanged(object sender, EventArgs e)
		{
			VigenereButtonGetMakeEnable();
		}
		private void VigenereTextBox2_TextChanged(object sender, EventArgs e)
		{
			VigenereButtonGetMakeEnable();
		}
		private void VigenereCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (VigenereCheckBox.Checked)
			{
				VigenereTextBox1.Enabled = true;
				VigenereTextBox2.Enabled = true;
				ColumnCheckBox.Checked = false;
			}
			else
			{
				VigenereTextBox1.Enabled = false;
				VigenereTextBox2.Enabled = false;
				VigenereButtonMake.Enabled = false;
				VigenereButtonGet.Enabled = false;
			}
			ColumnButtonGetMakeEnable();
		}
		private void VigenereButtonMake_Click(object sender, EventArgs e)
		{
			if (CheckTextBox(VigenereTextBox1) & CheckTextBox(VigenereTextBox2))
			{
				VigenereShif vigenereShif = new VigenereShif(VigenereTextBox1.Text, VigenereTextBox2.Text);
				ResTextBox.Text = vigenereShif.MakeShif();
				VigenereTextBox1.Text = vigenereShif.GetKey();
			}
			else
			{
				VigenereButtonGetMakeEnable();
			}
		}
		private void VigenereButtonGet_Click(object sender, EventArgs e)
		{
			if (CheckTextBox(VigenereTextBox1) & CheckTextBox(VigenereTextBox2))
			{
				VigenereShif vigenereShif = new VigenereShif(VigenereTextBox1.Text, VigenereTextBox2.Text);
				ResTextBox.Text = vigenereShif.GetStart();
			}
			else
			{
				VigenereButtonGetMakeEnable();
			}
		}
		private void ResTextBox_TextChanged(object sender, EventArgs e)
		{
			if (ResTextBox.Text.Length > 0)
			{
				SaveButton.Enabled = true;
			}
			else
			{
				SaveButton.Enabled = false;
			}
		}
		private void SaveButton_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					File.WriteAllText(saveFileDialog.FileName, ResTextBox.Text);
					MessageBox.Show("Файл успешно сохранен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		private void LoadButton_Click(object sender, EventArgs e)
		{
			Stream st;
			OpenFileDialog d1 = new OpenFileDialog();
			if (d1.ShowDialog() == DialogResult.OK)
			{
				if ((st = d1.OpenFile()) != null)
				{
					string file = d1.FileName;
					string str = File.ReadAllText(file);
					VigenereTextBox2.Text = str;
					ColumnTextBox3.Text = str;
				}
			}
		}
	}
}
