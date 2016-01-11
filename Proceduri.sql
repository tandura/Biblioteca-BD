drop procedure if exists adauga_utilizator;

DELIMITER //
CREATE PROCEDURE adauga_utilizator(_username varchar(45),_password varchar(45), _nume varchar(45), _prenume varchar(45), _ocupatie varchar(45), _sex enum('F','M'), _oras varchar(45), _codPostal VARCHAR(6), _strada varchar(45), _email varchar(45), _telefon VARCHAR(10),out _mesaj varchar(45), out _index int)   
    BEGIN 
	START TRANSACTION;
		set @idAdresaData = null, @idOcupatieData = null, @idPrivilegiuDat = null;
        select @idPrivilegiuDat := idPrivilegii from Privilegii where Nume = "user";
		select @NumarUtilizatori := count(*) from Utilizator where username = _username;
			
		IF(@NumarUtilizatori > 0) THEN 	
			BEGIN
			select "Nume de utilizator existent" into _mesaj;
			select null into _index;
			COMMIT;
			END;
		ELSE
            begin
			if(_oras is not null) then
                begin
				select @numarDeOrase := count(*) from Orase where nume = _oras;
				if(@numarDeOrase > 0) then
                    begin
					select @idOrasDat := idOrase from Orase where nume = _oras;
					select @numarAdrese := count(*) from Adrese where idOras = @idOrasDat and Strada = _strada and CodPostal = _codPostal;
					if(@numarAdrese > 0) then
						begin
						select @idAdresaData := idAdrese from Adrese where idOras = @idOrasDat and Strada = _strada and CodPostal = _codPostal;
						end;
					else
						begin
                        insert into Adrese(idOras, Strada, CodPostal)
						values(@idOrasDat, _strada, _codPostal);
                        
                        select @idAdresaData := idAdrese from Adrese where idOras = @idOrasDat and Strada = _strada and CodPostal = _codPostal;
                        end;
					end if;
                    end;
				else
                    begin
                    insert into Orase(Nume)
                    values(_oras);
                    
                    select @idOrasDat := idOrase from Orase where nume = _oras;
                    
                    insert into Adrese(idOras, Strada, CodPostal)
					values(@idOrasDat, _strada, _codPostal);
                    
					select @idAdresaData := idAdrese from Adrese where idOras = @idOrasDat and Strada = _strada and CodPostal = _codPostal;
                    end;
				end if;
                end;
			end if;
            if(_ocupatie is not null) then
				begin
                select @numarOcupatiiDate := count(*) from Ocupatii where Nume = _ocupatie;
				if(@numarOcupatiiDate > 0) then
					begin
					select @idOcupatieData := idOcupatie from Ocupatii where Nume = _ocupatie;
					end;
				else
					begin
					insert into Ocupatii(Nume)
					values (_ocupatie);
					
					select @idOcupatieData := idOcupatie from Ocupatii where Nume = _ocupatie;
					end;
				end if;
                end;
			else
				begin
                Set @idOcupatieData = null;
                end;
			end if;
            insert into Utilizator(Username,Parola,Nume,Prenume,Sex,idAdresa,NrTelefon,AdresaEmail,idOcupatie,idPrivilegiu)
            values (_username,_password,_nume,_prenume,_sex,@idAdresaData,_telefon,_email,@idOcupatieData,@idPrivilegiuDat);
            select idUtilizator into _index from Utilizator where Username = _username;
			select "ok" into _mesaj;
            COMMIT;
            end;
		END IF;
	END //
DELIMITER ;

call adauga_utilizator('yo','asa','Domnita','Dan','Prostanac','M','Turda','401231','Macilor',null,null,@msg,@indexul);
select @msg,@indexul;

update utilizator set idPrivilegiu = 1 WHERE idUtilizator=1;

drop procedure if exists adauga_editura;
DELIMITER //
CREATE PROCEDURE adauga_editura(_nume varchar(45))   
    BEGIN 
	START TRANSACTION;
    insert into editura(Nume) value (_nume);
    commit;
    end//
delimiter ;


drop procedure if exists adauga_autor;
DELIMITER //
CREATE PROCEDURE adauga_autor(_nume varchar(45))   
    BEGIN 
	START TRANSACTION;
    insert into autor(Nume) value (_nume);
    commit;
    end//
delimiter ;

drop procedure if exists adauga_colectie;
DELIMITER //
CREATE PROCEDURE adauga_colectie(_nume varchar(45))   
    BEGIN 
	START TRANSACTION;
    insert into colectii(Nume) value (_nume);
    commit;
    end//
delimiter ;

