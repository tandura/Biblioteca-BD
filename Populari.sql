insert into autor(Nume)
values('Gustave Flaubert'),('George Calinescu'),('Vladimir Nabucov'),('Ion Creanga'),('Liviu Rebreanu'),
('Vasile Alecsandri'),('Camil Petrescu'),('Mihai Eminescu'),('Lev Tolstoi'),('Ion Luca Caragiale')
,('Honeore de Balzac'),('F.M. Dostoievski'),('Charles Dickens'),('Alexandre Dumas'),('Thomas Hardy'),
('George Bacovia'),('Gabriel Garcia Marquez'),('George R.R. Martin'),('Stephen King'),
('Hans Christian Andersen'),('J. K. Rowling'),('Mihaela Belcin'),('Ioan Lazarescu'),('Anca Voleanov');

insert into colectii(Nume)
values('Colectia de lux'),('Biblioteca pentru toti'),('Colectionarului'),('Nautilus'),('Nobel'),('Curtea veche'),('Harry Potter'),('Biblioteca Polirom');

insert into editura(Nume)
values('Adantis'),('Babel'),('Cartea copiilor'),('Codex'),('Dacia'),('Pi'),('Euristica'),('Galaxia copiilor'),
('Integral'),('Nemira'),('Teora'),('Art'),('Cartex'),('Herald '),('Humanitas'),('Leda'),('Litera '),('Polirom '),
('RAO'),('Univers Enciclopedic'),('Adevarul'),('Corint');

insert into gen(Nume)
values('Fictiune pentru copii'),('Fantezie'),('Mister'),('Literatura moderna'),('S.F.'),('Literatura Clasica'),('Beletristica'),
('Poezie'),('Filosofie'),('Enciclopedii'),('Dictionare'),('Limbi Straine'),('Calculatoare'),('Psihologie '),
('Stiinta'),('Technica'),('Istorie'),('Arta'),('Medicina'),('Politica'),('Economie'),('Sociologie'),('Mass media');


delete from carteautor where Carte_idCarte='3';
delete from carte where idCarte='3';

insert into carte(Titlu,ISBN,Rezumat,DataAparitie,ImagineCoperta,NrPagini,idColectie,NotaCarte,NrCarti) 
values
('Lolita','99921-58-10-7',null,'2004-08-14',null, 321,null,4,3),
('Madame Bovary','978-3-16-148410-0',null,'2004-08-14',null, 321,null,4,3),
('Enigma Otiliei','978-3-16-148410-0',null,'2004-08-14',null, 321,null,4,3),
('Ultima Noapte','978-3-16-148410-0',null,'2004-08-14',null, 321,null,4,3),
('O Scrisoare Pierduta','978-3-16-148410-0',null,'2004-08-14',null, 321,null,4,3),
('Idiotul','978-3-16-148410-0',null,'2004-08-14',null, 321,null,4,3),
('Marile sperante ','978-3-16-148410-0',null,'2004-08-14',null, 321,null,4,3);


insert into carteautor(Carte_idCarte,Autor_idAutor) 
values ('1','1'),('2','2'),('3','3'),('4','4'),('5','5'),('6','6'),('7','7'),('1','4');

