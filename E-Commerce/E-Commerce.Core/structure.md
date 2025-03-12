# Structure de la couche E-Commerce.Core

La couche Core contient les entités de domaine et les règles métier fondamentales de l'application.

## Structure des Dossiers

```
E-Commerce.Core/
├── Common/
│   └── BaseEntity.cs               # Classe de base pour toutes les entités avec Id, CreatedDate, LastModifiedDate
│
├── Identity/
│   └── ApplicationUser.cs          # Entité utilisateur héritant de IdentityUser
│
├── Orders/
│   ├── OrderHeader.cs              # En-tête de commande (informations principales)
│   └── OrderDetails.cs             # Détails de la commande (produits commandés)
│
├── Products/
│   ├── Product.cs                  # Entité produit
│   └── Category.cs                 # Catégories de produits
│
├── Cart/
│   ├── CartHeader.cs               # En-tête du panier
│   └── CartDetails.cs              # Détails du panier (produits)
│
└── Coupon/
    └── Coupon.cs                   # Gestion des coupons de réduction
```

## Description des Composants

### Common
- **BaseEntity.cs**: Classe abstraite définissant les propriétés communes à toutes les entités
  - `Id` (Guid)
  - `CreatedDate` (DateTime)
  - `LastModifiedDate` (DateTime?)

### Identity
- **ApplicationUser.cs**: Extension de IdentityUser avec des propriétés supplémentaires
  - `FirstName`
  - `LastName`
  - `Address`

### Orders
- **OrderHeader.cs**: Informations principales de la commande
  - Relations avec User et Coupon
  - Informations de paiement et de livraison
  - Statut de la commande

- **OrderDetails.cs**: Détails des produits commandés
  - Relation avec OrderHeader et Product
  - Quantité et prix au moment de la commande

### Products
- **Product.cs**: Information sur les produits
  - Nom, description, prix
  - Relation avec Category
  - Image URL

- **Category.cs**: Catégorisation des produits
  - Nom de la catégorie
  - Collection de produits associés

### Cart
- **CartHeader.cs**: En-tête du panier d'achat
  - Relation avec User et Coupon
  - Informations générales du panier

- **CartDetails.cs**: Produits dans le panier
  - Relation avec CartHeader et Product
  - Quantité sélectionnée

### Coupon
- **Coupon.cs**: Gestion des réductions
  - Code du coupon
  - Montant de la réduction
  - Montant minimum d'achat
  - ID Stripe associé

## Relations entre les Entités

1. **User -> Orders/Cart**
   - Un utilisateur peut avoir plusieurs commandes
   - Un utilisateur peut avoir un panier

2. **Product -> Category**
   - Un produit appartient à une catégorie
   - Une catégorie peut avoir plusieurs produits

3. **OrderHeader -> OrderDetails**
   - Une commande peut avoir plusieurs détails
   - Chaque détail est lié à un produit

4. **CartHeader -> CartDetails**
   - Un panier peut contenir plusieurs produits
   - Chaque détail est lié à un produit

5. **Coupon -> Orders/Cart**
   - Un coupon peut être appliqué à plusieurs commandes
   - Un coupon peut être appliqué à plusieurs paniers

## Particularités Techniques

- Utilisation de Guid comme type d'ID pour toutes les entités
- Annotations de données pour la validation
- Navigation properties pour les relations Entity Framework
- Support pour le suivi des modifications (audit)
