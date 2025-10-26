ChronoCraftBack
Backend du site web de l’auteure belge Nicole Nisol

🌿 Présentation

ChronoCraftBack est le projet backend du site web de Nicole Nisol, auteure originaire du charmant village de Wihéries, en région Wallonne (Belgique).
Passionnée par les mots depuis toujours, Nicole s’est lancée tardivement dans l’écriture, donnant vie à des récits poétiques et profonds, empreints de sincérité.

Ce projet a pour objectif de fournir une base technique moderne, stable et évolutive pour la présence en ligne de l’auteure :

d’abord à travers un site vitrine (V1),

puis une boutique en ligne (V2) pour la vente de ses ouvrages en version papier et numérique.

⚙️ Stack technique
Composant	Technologie
Backend	.NET 8 / C#
ORM	Entity Framework Core
Base de données	Microsoft SQL Server
Architecture	Clean Architecture (Domain / Application / Infrastructure / API)
Tests	xUnit
Intégration continue	GitHub Actions
Hébergement (à venir)	Render / Railway (V2)
🚀 Installation locale

Cloner le dépôt

git clone https://github.com/Rusius02/ChronoCraftBack.git
cd ChronoCraftBack


Configurer la base de données

Vérifie que SQL Server (ou SQL Express) tourne localement.

Mets à jour la chaîne de connexion dans appsettings.Development.json du projet API principal.

Appliquer les migrations

dotnet ef database update


Lancer l’API

dotnet run --project NisolNicole


Tester dans le navigateur ou via Swagger

L’API démarre généralement sur https://localhost:5001

L’interface Swagger sera disponible sur /swagger

🧩 Structure du projet
ChronoCraftBack/
├── NisolNicole.sln
├── Domain/              # Entités métiers et logique pure
├── Application/         # Cas d’usage (CQRS, Services)
├── Infrastructure/      # Accès données (EF Core, SQL Server)
├── NisolNicole/         # API principale (.NET Web API)
└── Tests/               # Tests unitaires

🔁 Intégration continue (CI/CD)

Le projet inclut un workflow GitHub Actions situé dans
.github/workflows/daily-build.yml.

Ce pipeline :

🔄 Compile automatiquement toute la solution chaque nuit (2h UTC / 3h FR)

🧪 Exécute les tests unitaires


🗺️ Roadmap
Version	Objectif	Description
V1 – Site vitrine	✅ En cours	Présentation de l’auteure, galerie, redirection vers site d’achat papier
V2 – Boutique en ligne	⏳ À venir	Intégration de Stripe pour les paiements sécurisés, gestion du stock et des commandes
V3 – Espace Lecteurs	💡 Idée future	Gestion de comptes, avis, et téléchargement d’extraits numériques
❤️ Crédits

Développé avec passion par Rusius02

pour soutenir l’univers littéraire de Nicole Nisol, auteure wallonne.
