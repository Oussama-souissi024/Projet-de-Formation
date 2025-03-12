# E-Commerce Application Documentation

## 📋 Table des matières
1. [Vue d'ensemble](#vue-densemble)
2. [Architecture](#architecture)
3. [Configuration](#configuration)
4. [Fonctionnalités principales](#fonctionnalités-principales)
5. [Base de données](#base-de-données)
6. [Sécurité](#sécurité)
7. [Tests](#tests)
8. [Déploiement](#déploiement)
9. [API Documentation](#api-documentation)

## Vue d'ensemble
L'application E-Commerce est une solution complète de commerce électronique construite avec ASP.NET Core MVC. Elle suit les principes de l'architecture propre (Clean Architecture) et implémente les meilleures pratiques de développement.

### Technologies utilisées
- **Backend**: ASP.NET Core 7.0
- **ORM**: Entity Framework Core
- **Base de données**: SQL Server
- **Authentication**: ASP.NET Core Identity
- **Payment Processing**: Stripe
- **Frontend**: Bootstrap, jQuery
- **Tests**: xUnit

## Architecture
L'application suit une architecture en couches avec une séparation claire des responsabilités :

### 1. Core Layer (E-Commerce.Core)
- Contient les entités métier et les interfaces
- Indépendant des autres couches
- Définit les contrats pour les repositories et services

### 2. Infrastructure Layer (E-Commerce.Infrastructure)
- Implémente les interfaces définies dans Core
- Gère l'accès aux données et services externes
- Contient la configuration de la base de données

### 3. Application Layer (E-Commerce.Application)
- Contient la logique métier
- Gère les cas d'utilisation
- Implémente les services
- Gère la transformation des données (DTOs)

### 4. Presentation Layer (E-Commerce)
- Interface utilisateur MVC
- Gestion des routes
- Validation des entrées
- Gestion des sessions

## Configuration

### Base de données
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=E_CommerceFormation;User Id=sa;Password=sa123456;TrustServerCertificate=true"
  }
}
```

### Stripe
```json
{
  "Stripe": {
    "SecretKey": "sk_test_..."
  }
}
```

## Fonctionnalités principales

### 1. Gestion des utilisateurs
- Inscription/Connexion
- Gestion des rôles
- Profil utilisateur

### 2. Catalogue de produits
- Liste des produits
- Détails des produits
- Catégories
- Recherche et filtrage

### 3. Panier d'achat
- Ajout/Suppression d'articles
- Mise à jour des quantités
- Calcul du total
- Application des coupons

### 4. Commandes
- Création de commande
- Suivi de commande
- Historique des commandes
- États des commandes

### 5. Paiements
- Intégration Stripe
- Paiement sécurisé
- Gestion des remboursements
- Gestion des coupons

## Base de données

### Entités principales
1. **ApplicationUser**
   - Informations utilisateur
   - Données d'authentification

2. **Product**
   - Informations produit
   - Prix et stock
   - Catégorie

3. **Category**
   - Hiérarchie des catégories
   - Métadonnées

4. **Order**
   - OrderHeader
   - OrderDetails
   - Statut et suivi

5. **Cart**
   - CartHeader
   - CartDetails
   - Session utilisateur

## Sécurité

### Authentication
- ASP.NET Core Identity
- JWT pour l'API
- Gestion des sessions

### Autorisation
- Rôles utilisateur
- Politiques d'accès
- Validation des tokens

### Protection des données
- Chiffrement des données sensibles
- Validation des entrées
- Protection CSRF

### Étapes de déploiement
1. Publication de l'application
2. Configuration de la base de données
3. Configuration des variables d'environnement
4. Configuration du serveur web

## Maintenance

### Logging
- Logs applicatifs
- Logs d'erreurs
- Monitoring des performances

### Sauvegarde
- Sauvegarde de la base de données
- Sauvegarde des fichiers
- Plan de reprise

