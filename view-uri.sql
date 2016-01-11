CREATE VIEW `Afisare Carte` AS SELECT *
FROM Carte
WHERE idCarte = 1;

CREATE VIEW `Afisare Bibliotecare` AS SELECT Nume,Prenume
FROM Utilizator
WHERE idPrivilegiu=2;