using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using FormCollection.Dto;

namespace StudentTest
{
    public partial class FormSearchByStudent : Form
    {
        public FormSearchByStudent()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        GlendaleLibrarySystemEntities1 db = new GlendaleLibrarySystemEntities1();

        private IEnumerable<BookDto> MapToBookDto(IEnumerable<Book> books)
        {
            return books
                .Select(book => new BookDto()
                {
                    BookCode = book.BookCode,
                    Name = string.Join(" ", book.Author.FirstName, book.Author.LastName),
                    Title = book.Title,
                    Publisher = book.Publisher.Publisher1,
                    Category = book.Category.Category1,
                    SubCategory = book.SubCategory.SubCategory1
                });
        }

        private void btnSearchS_Click(object sender, EventArgs e)
        {
            List<BookDto> books = new List<BookDto>();

            if (cmbSearchS.Text == "Title")
            {
                books = MapToBookDto(db.Book.Where(m => m.Title.Contains(txtSearchS.Text))).ToList();
                int count = books.Count();

                if (count > 0)
                {
                    MessageBox.Show("Search Successful", "Title Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvSearchS.DataSource = books;
                }
                else
                {
                    MessageBox.Show("Title Not Found", "Title Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            else if (cmbSearchS.Text == "Category")
            {
                books = MapToBookDto(db.Book.Where(m => m.Category.Category1.Contains(txtSearchS.Text))).ToList();
                int count = books.Count();

                if (count > 0)
                {
                    MessageBox.Show("Search Successful", "Category Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvSearchS.DataSource = books;
                }
                else
                {
                    MessageBox.Show("Category Not Found", "Category Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            else if (cmbSearchS.Text == "SubCategory")
            {
                books = MapToBookDto(db.Book.Where(m => m.SubCategory.SubCategory1.Contains(txtSearchS.Text))).ToList();
                int count = books.Count();


                if (count > 0)
                {
                    MessageBox.Show("Search Successful", "SubCategory Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvSearchS.DataSource = books;
                }
                else
                {
                    MessageBox.Show("SubCategory Not Found", "SubCategory Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            else if (cmbSearchS.Text == "Author")
            {
                books = MapToBookDto(db.Book.Where(m => m.Author.FirstName.Contains(txtSearchS.Text) || m.Author.LastName.Contains(txtSearchS.Text))).ToList();
                int count = books.Count();


                if (count > 0)
                {
                    MessageBox.Show("Search Successful", "Author Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvSearchS.DataSource = books;
                }
                else
                {
                    MessageBox.Show("Author Not Found", "Author Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
