using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StudentTest.Database;
using FormCollection.Dto;
using FormCollection;
using StudentTestEntity = StudentTest.Database;


namespace StudentTest
{
    public partial class FormOrder : Form
    {
        GlendaleLibrarySystemEntities2 db = new GlendaleLibrarySystemEntities2();


        


        public FormOrder()
        {
            InitializeComponent();
            
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void cmbSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSearchBy_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearchBy_Click(object sender, EventArgs e)
        {
            List<BookDto> books = new List<BookDto>();

            if (string.IsNullOrWhiteSpace(txtSearchBy.Text))
            {
                MessageBox.Show("Search bar is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbSearchBy.Text == "Title")
            {
                // Use the alias to reference the conflicting namespace
                books = MapToBookDto(db.Set<StudentTest.Database.Book>().Where(m => m.Title.Contains(txtSearchBy.Text))).ToList();
                int count = books.Count();

                if (count > 0)
                {
                    MessageBox.Show("Search Successful", "Title Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvSearch.DataSource = books;
                }
                else
                {
                    MessageBox.Show("Title Not Found", "Title Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private IEnumerable<BookDto> MapToBookDto(IEnumerable<Database.Book> books)
        {
            return books
                .Select(book => new BookDto()
                {
                    BookId = book.BookId,
                    BookCode = book.BookCode,
                    Name = string.Join(" ", book.Author.FirstName, book.Author.LastName),
                    Title = book.Title,
                    Publisher = book.Publisher.Publisher1,
                    Category = book.Category.Category1,
                    SubCategory = book.SubCategory.SubCategory1
                });
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<BookDto> bookCart = new List<BookDto>();

            if (dgvSearch.SelectedRows.Count > 0)
            {
                int bookId = Convert.ToInt32(dgvSearch.SelectedRows[0].Cells["BookId"].Value);

                if (bookCart.Any(m => m.BookId == bookId))
                {
                    MessageBox.Show("Book is already added to cart!");
                }
                else if (bookCart.Count() == 3)
                {
                    MessageBox.Show("Cannot add book. Maximum is 3 only");
                }
                else
                {
                    var book = db.Book.Find(bookId);
                    bookCart.Add(new BookDto()
                    {
                        BookId = bookId,
                        Title = book.Title,
                        BookCode = book.BookCode,
                        Publisher = book.Publisher.Publisher1,
                        Category = book.Category.Category1,
                        SubCategory = book.SubCategory.SubCategory1,
                        Name = String.Join(" ", book.Author.FirstName, book.Author.LastName)
                    });


                    dgvCart.DataSource = null;
                    dgvCart.DataSource = bookCart;
                }

            }
            else
            {
                MessageBox.Show("No row selected.");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string orderID = GenerateOrderCode();
            txtOrderCode.Text = orderID;
        }

        private HashSet<string> usedOrderCodes = new HashSet<string>();
        private string GenerateOrderCode()
        {
            string prefix = "ORD";
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();

            while (true)
            {
                string orderCode = new string(Enumerable.Repeat(characters, 7)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

                if (!usedOrderCodes.Contains(orderCode))
                {
                    usedOrderCodes.Add(orderCode);
                    string finalOrderCode = $"{prefix}{orderCode}";
                    return finalOrderCode;
                }

            }
        }
    }
}
