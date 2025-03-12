using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entities.Cart
{
    //classe de modèle métier
    public class Cart
    {
        // Métadonnées du panier (utilisateur, coupon, etc.)
        public CartHeader CartHeader { get; set; }

        // Liste des produits dans le panier
        public IEnumerable<CartDetails>? CartDetails { get; set; }
    }
}
