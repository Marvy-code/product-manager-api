CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "ProductCategories" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Description" text NULL,
    CONSTRAINT "PK_ProductCategories" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221117155935_InitialCreate', '7.0.0');

COMMIT;

START TRANSACTION;

CREATE TABLE "Products" (
    "Id" uuid NOT NULL,
    "CategoryId" uuid NOT NULL,
    "Name" text NOT NULL,
    "Description" text NULL,
    "Price" numeric NOT NULL,
    "Quantity" integer NOT NULL,
    CONSTRAINT "PK_Products" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Products_ProductCategories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "ProductCategories" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Products_CategoryId" ON "Products" ("CategoryId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221118152742_AddProductObjects', '7.0.0');

COMMIT;

