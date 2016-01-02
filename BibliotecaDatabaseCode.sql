-- MySQL Workbench Forward Engineering
-- -----------------------------------------------------
-- Schema biblioteca
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema biblioteca
-- -----------------------------------------------------
drop schema if exists `biblioteca`;
CREATE SCHEMA IF NOT EXISTS `biblioteca` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci ;
USE `biblioteca` ;

-- -----------------------------------------------------
-- Table `biblioteca`.`Colectii`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteca`.`Colectii` (
  `idColectii` INT NOT NULL AUTO_INCREMENT,
  `Nume` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idColectii`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteca`.`Carte`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteca`.`Carte` (
  `idCarte` INT NOT NULL AUTO_INCREMENT,
  `Titlu` VARCHAR(45) NOT NULL,
  `ISBN` CHAR(14) NOT NULL,
  `Rezumat` MEDIUMTEXT NULL,
  `DataAparitie` DATE NOT NULL,
  `ImagineCoperta` MEDIUMBLOB NULL,
  `NrPagini` INT UNSIGNED NOT NULL,
  `idColectie` INT NULL,
  `NotaCarte` TINYINT(1) NOT NULL,
  `NrCarti` INT NOT NULL,
  PRIMARY KEY (`idCarte`),
  INDEX `colectieCarte_idx` (`idColectie` ASC),
  CONSTRAINT `colectieCarte`
    FOREIGN KEY (`idColectie`)
    REFERENCES `biblioteca`.`Colectii` (`idColectii`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
ROW_FORMAT = DEFAULT;


-- -----------------------------------------------------
-- Table `biblioteca`.`Autor`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteca`.`Autor` (
  `idAutor` INT NOT NULL AUTO_INCREMENT,
  `Nume` VARCHAR(45) NOT NULL,
  `Prenume` VARCHAR(45) NOT NULL,
  `Origine` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idAutor`),
  UNIQUE INDEX `idAutor_UNIQUE` (`idAutor` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteca`.`CarteAutor`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteca`.`CarteAutor` (
  `Carte_idCarte` INT NOT NULL,
  `Autor_idAutor` INT NOT NULL,
  PRIMARY KEY (`Carte_idCarte`, `Autor_idAutor`),
  INDEX `fk_Carte_has_Autor_Autor1_idx` (`Autor_idAutor` ASC),
  INDEX `fk_Carte_has_Autor_Carte1_idx` (`Carte_idCarte` ASC),
  CONSTRAINT `fk_Carte_has_Autor_Carte1`
    FOREIGN KEY (`Carte_idCarte`)
    REFERENCES `biblioteca`.`Carte` (`idCarte`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Carte_has_Autor_Autor1`
    FOREIGN KEY (`Autor_idAutor`)
    REFERENCES `biblioteca`.`Autor` (`idAutor`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteca`.`Editura`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteca`.`Editura` (
  `idEditura` INT NOT NULL AUTO_INCREMENT,
  `Nume` VARCHAR(45) NOT NULL,
  `NrCarti` INT NULL,
  PRIMARY KEY (`idEditura`),
  UNIQUE INDEX `idEditura_UNIQUE` (`idEditura` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteca`.`CarteEditura`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteca`.`CarteEditura` (
  `Carte_idCarte` INT NOT NULL,
  `Editura_idEditura` INT NOT NULL,
  PRIMARY KEY (`Carte_idCarte`, `Editura_idEditura`),
  INDEX `fk_Carte_has_Editura_Editura1_idx` (`Editura_idEditura` ASC),
  INDEX `fk_Carte_has_Editura_Carte1_idx` (`Carte_idCarte` ASC),
  CONSTRAINT `fk_Carte_has_Editura_Carte1`
    FOREIGN KEY (`Carte_idCarte`)
    REFERENCES `biblioteca`.`Carte` (`idCarte`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Carte_has_Editura_Editura1`
    FOREIGN KEY (`Editura_idEditura`)
    REFERENCES `biblioteca`.`Editura` (`idEditura`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteca`.`Gen`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteca`.`Gen` (
  `idGen` INT NOT NULL AUTO_INCREMENT,
  `Nume` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idGen`),
  UNIQUE INDEX `idGen_UNIQUE` (`idGen` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteca`.`CarteGen`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteca`.`CarteGen` (
  `Carte_idCarte` INT NOT NULL,
  `Gen_idGen` INT NOT NULL,
  PRIMARY KEY (`Gen_idGen`, `Carte_idCarte`),
  INDEX `fk_Carte_has_Gen_Gen1_idx` (`Gen_idGen` ASC),
  INDEX `fk_Carte_has_Gen_Carte1_idx` (`Carte_idCarte` ASC),
  CONSTRAINT `fk_Carte_has_Gen_Carte1`
    FOREIGN KEY (`Carte_idCarte`)
    REFERENCES `biblioteca`.`Carte` (`idCarte`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Carte_has_Gen_Gen1`
    FOREIGN KEY (`Gen_idGen`)
    REFERENCES `biblioteca`.`Gen` (`idGen`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteca`.`Orase`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteca`.`Orase` (
  `idOrase` INT NOT NULL AUTO_INCREMENT,
  `Nume` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idOrase`),
  UNIQUE INDEX `idOrase_UNIQUE` (`idOrase` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteca`.`Adrese`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteca`.`Adrese` (
  `idAdrese` INT NOT NULL AUTO_INCREMENT,
  `idOras` INT NOT NULL,
  `Strada` VARCHAR(45) not null,
  `CodPostal` VARCHAR(6) not null,
  PRIMARY KEY (`idAdrese`),
  UNIQUE INDEX `idAdrese_UNIQUE` (`idAdrese` ASC),
  INDEX `AdresaOras_idx` (`idOras` ASC),
  CONSTRAINT `AdresaOras`
    FOREIGN KEY (`idOras`)
    REFERENCES `biblioteca`.`Orase` (`idOrase`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteca`.`Ocupatii`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteca`.`Ocupatii` (
  `idOcupatie` INT NOT NULL AUTO_INCREMENT,
  `Nume` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idOcupatie`),
  UNIQUE INDEX `idtable1_UNIQUE` (`idOcupatie` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteca`.`Privilegii`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteca`.`Privilegii` (
  `idPrivilegii` INT NOT NULL AUTO_INCREMENT,
  `Nume` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idPrivilegii`),
  UNIQUE INDEX `idPrivilegii_UNIQUE` (`idPrivilegii` ASC))
ENGINE = InnoDB;

insert into `biblioteca`.`Privilegii`(Nume)
values("admin"),("bibliotecar"),("user");
-- -----------------------------------------------------
-- Table `biblioteca`.`Utilizator`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteca`.`Utilizator` (
  `idUtilizator` INT NOT NULL AUTO_INCREMENT,
  `Username` VARCHAR(45) NOT NULL,
  `Parola` VARCHAR(45) NOT NULL,
  `Nume` VARCHAR(45) NULL,
  `Prenume` VARCHAR(45) NULL,
  `Sex` ENUM('F', 'M') NULL,
  `idAdresa` INT NULL,
  `NrTelefon` VARCHAR(10) NULL,
  `AdresaEmail` VARCHAR(45) NULL,
  `idOcupatie` INT NULL,
  `NrCartiImprumutate` DECIMAL(1) NOT NULL DEFAULT 0,
  `idPrivilegiu` INT NOT NULL,
  PRIMARY KEY (`idUtilizator`),
  UNIQUE INDEX `idUtilizator_UNIQUE` (`idUtilizator` ASC),
  INDEX `UtilizatorAdresa_idx` (`idAdresa` ASC),
  INDEX `UtilizatorOcupatie_idx` (`idOcupatie` ASC),
  INDEX `UtilizatorPrivilegiu_idx` (`idPrivilegiu` ASC),
  CONSTRAINT `UtilizatorAdresa`
    FOREIGN KEY (`idAdresa`)
    REFERENCES `biblioteca`.`Adrese` (`idAdrese`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `UtilizatorOcupatie`
    FOREIGN KEY (`idOcupatie`)
    REFERENCES `biblioteca`.`Ocupatii` (`idOcupatie`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `UtilizatorPrivilegiu`
    FOREIGN KEY (`idPrivilegiu`)
    REFERENCES `biblioteca`.`Privilegii` (`idPrivilegii`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteca`.`Imprumuturi`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteca`.`Imprumuturi` (
  `idImprumut` INT NOT NULL AUTO_INCREMENT,
  `idUtilizator` INT NOT NULL,
  `idCarte` INT NOT NULL,
  `dataImprumut` DATE NOT NULL,
  `dataRestituire` DATE NOT NULL,
  INDEX `fk_Utilizator_has_Carte_Carte1_idx` (`idCarte` ASC),
  INDEX `fk_Utilizator_has_Carte_Utilizator1_idx` (`idUtilizator` ASC),
  PRIMARY KEY (`idImprumut`),
  UNIQUE INDEX `idImprumut_UNIQUE` (`idImprumut` ASC),
  CONSTRAINT `fk_Utilizator_has_Carte_Utilizator1`
    FOREIGN KEY (`idUtilizator`)
    REFERENCES `biblioteca`.`Utilizator` (`idUtilizator`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Utilizator_has_Carte_Carte1`
    FOREIGN KEY (`idCarte`)
    REFERENCES `biblioteca`.`Carte` (`idCarte`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteca`.`IstoricImprumuturi`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteca`.`IstoricImprumuturi` (
  `idIstoricImprumuturi` INT NOT NULL AUTO_INCREMENT,
  `idUtilizator` INT NOT NULL,
  `idCarte` INT NOT NULL,
  PRIMARY KEY (`idIstoricImprumuturi`),
  UNIQUE INDEX `idIstoricImprumuturi_UNIQUE` (`idIstoricImprumuturi` ASC),
  INDEX `istoricUtilizatorCarte_idx` (`idUtilizator` ASC),
  INDEX `istoricCarte_idx` (`idCarte` ASC),
  CONSTRAINT `istoricUtilizator`
    FOREIGN KEY (`idUtilizator`)
    REFERENCES `biblioteca`.`Utilizator` (`idUtilizator`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `istoricCarte`
    FOREIGN KEY (`idCarte`)
    REFERENCES `biblioteca`.`Carte` (`idCarte`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteca`.`wishlist`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteca`.`wishlist` (
  `idwishlist` INT NOT NULL AUTO_INCREMENT,
  `idUtilizator` INT NOT NULL,
  `idCarte` INT NOT NULL,
  PRIMARY KEY (`idwishlist`),
  UNIQUE INDEX `idtable1_UNIQUE` (`idwishlist` ASC),
  INDEX `wishlistUtilizator_idx` (`idUtilizator` ASC),
  INDEX `wishlistCarte_idx` (`idCarte` ASC),
  CONSTRAINT `wishlistUtilizator`
    FOREIGN KEY (`idUtilizator`)
    REFERENCES `biblioteca`.`Utilizator` (`idUtilizator`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `wishlistCarte`
    FOREIGN KEY (`idCarte`)
    REFERENCES `biblioteca`.`Carte` (`idCarte`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

