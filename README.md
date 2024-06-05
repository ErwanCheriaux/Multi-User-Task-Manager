# Multi-User Task Manager

## Quick start

Start SQL server database and .NET server with Docker compose, required docker installed.
SQL server run at http://localhost:1433
.NET server listen at http://localhost:5280

```
docker compose up -d --build
```

(optional)
Insert common categories into database, requiered sqlcmd installed.

```
sqlcmd -S localhost -d MultiUserTaskManagerDb -U sa -P '<YourStrong@Passw0rd>' -i .\scripts\InsertCommonCategories.sql
```

Start client running at http://localhost:5173, requiered npm installed.

```
cd frontend
npm install
npm run dev -- --open
```

## Statement

Un utilisateur possède : mail/identifiant, nom, prénom, password
Une tâche possède : un libellé, une catégorie, une date de fin, un statut todo/done
(vous pouvez ajouter des propriétés qui vous semblent utiles)

Le site doit avoir un module d'inscription et une gestion de sécurité, pour que les données d'un utilisateur ne soient pas visibles des autres.
Il doit y avoir un listing de catégorie "générique pour tout le monde", et un user peut en plus créer son propre référentiel.
Ensuite une gestion "classique" des tâches, par utilisateur (vision / création / update / ...)

Si vous utilisez une BDD non mémoire, merci de me fournir le script de création, et son type.
