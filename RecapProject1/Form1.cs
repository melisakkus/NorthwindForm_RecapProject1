using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecapProject1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListCategories();
            ListProducts();
        }

        private void ListProducts()
        {
            using (NorthwindContext context = new NorthwindContext()) //1-ürünleri listeleyelim
            {
                dgwProduct.DataSource = context.Products.ToList();
                //veritabanında context'in Products'ına Select * From sorgusu gönderir, o sorgu da Product döndürür, Product.cs de ne varsa onları Select eder, listeye döndürelim
            }
        }

        private void ListCategories()   //2-kategorileri listeleyelim
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                cbxCategory.DataSource = context.Categories.ToList();
                cbxCategory.DisplayMember = "CategoryName"; //görünecek değer : kategori ismi
                //ComboBox'ta kategorilerin isimlerinin (CategoryName) gösterileceğini belirtir.
                cbxCategory.ValueMember = "CategoryId"; // gösterdiğin değeri id den al
                //her bir kategori seçildiğinde arka planda kategorinin ID'sinin (CategoryId) kullanılacağını belirtir.
            }
        }

        //DisplayMember, kontrol üzerinde kullanıcıya gösterilecek olan değeri belirler.
        //Bu özellik, veri kaynağındaki hangi alanın kullanıcıya gösterileceğini tanımlar.

        //ValueMember, kontrolün arka planda hangi değeri taşıyacağını belirler.
        //Bu özellik, veri kaynağındaki hangi alanın kontrolün arka plan değeri olarak kullanılacağını tanımlar.

        //Eğer ValueMember özelliğini ayarlamazsanız, SelectedValue özelliği anlamlı bir değer döndürmez ve
        //bunun yerine SelectedItem özelliğini kullanarak tüm nesneyi almak zorunda kalırsınız.
        //Bu, ek işlemler gerektirir ve kodu daha karmaşık hale getirir.

        private void ListProductsByCategory(int categoryId)
        {
            using (NorthwindContext context = new NorthwindContext()) 
            {
                dgwProduct.DataSource = context.Products.Where(p => p.CategoryId == categoryId).ToList();
            }
        }
        //Bu metot, belirli bir kategori ID'sine ait ürünleri veri tabanından getirir ve
        //bunları bir veri ızgarasında (dgwProduct) görüntüler. Bu metot, veri getirme ve görüntüleme işlemlerini gerçekleştirir.

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListProductsByCategory(Convert.ToInt32(cbxCategory.SelectedValue));
            }
            catch
            {

            }
        }
        //Bu metot, ComboBox'ta bir kategori seçildiğinde tetiklenen olaydır. Kullanıcı bir kategori seçtiğinde,
        //SelectedValue'yu alır ve bu değeri ListProductsByCategory metoduna ileterek, ilgili kategoriye ait ürünlerin görüntülenmesini sağlar.

        private void dgwProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            string key = tbxSearch.Text;
            if (string.IsNullOrEmpty(key))
            {
                ListProducts();
            }
            else
            {
                ListProductsByProductName(tbxSearch.Text);

            }
        }

        private void ListProductsByProductName(string key)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                dgwProduct.DataSource = context.Products.Where(p => p.ProductName.ToLower().Contains(key.ToLower())).ToList();
            }
        }
    }
}
//single responsibility prensibi ; metotları olabildiğince ayırmak gerekir
//kategorileride load formda görmek istiyorum, bunun için kategorileride sınıf olarak eklemem ve contexte dahil etmem gerekli
