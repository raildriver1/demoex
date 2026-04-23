using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp3.Elements;

namespace WpfApp3.ViewModel
{
    public class ProductViewModel
    {

        public ProductViewModel(Product product)
        {
            IdProd = product.IdProd;
            Article = product.Article;
            ProdName = product.ProdName;
            Price = product.Price;
            Sale = product.Sale;
            Count = product.Count;
            Descrip = product.Descrip;
            Image = product.Image;
            SupId = product.SupId;
            ManufId = product.ManufId;
            CatId = product.CatId;
            Cat = product.Cat;
            Manuf = product.Manuf;
            ProdNameNavigation = product.ProdNameNavigation;
            Sup = product.Sup;

            getBackground();
            getPhoto();
        }

        public int IdProd { get; set; }

        public string Article { get; set; } = null!;

        public int ProdName { get; set; }

        public decimal Price { get; set; }

        public int Sale { get; set; }

        public int Count { get; set; }

        public string? Descrip { get; set; }

        public string? Image { get; set; }

        public int SupId { get; set; }

        public int ManufId { get; set; }

        public int CatId { get; set; }

        public Category Cat { get; set; } = null!;

        public Manufacrurer Manuf { get; set; } = null!;

        public Prodname ProdNameNavigation { get; set; } = null!;

        public ICollection<Prodorder> Prodorders { get; set; } = new List<Prodorder>();

        public Supplier Sup { get; set; } = null!;

        public string ImagePath => "Images/" + Image;

        public Brush background { get; set; }

        public void getBackground()
        {
            if (Sale >= 15)
            {
                background = (Brush)new BrushConverter().ConvertFromString("#2e8b57");
                return;
            } else if (Count == 0)
            {
                background = Brushes.LightBlue;
                return;
            } else
            {
                background = (Brush)new BrushConverter().ConvertFromString("#7fff00");
                return;
            }
        }

        public void getPhoto()
        {
            if (!string.IsNullOrEmpty(Image) || Image != "")
                return;

            Image = "picture.png";
        }

    }
}
