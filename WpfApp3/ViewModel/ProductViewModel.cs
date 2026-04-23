using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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

    }
}
