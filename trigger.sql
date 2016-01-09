DELIMITER //

create TRIGGER creste_nr_carti_editura AFTER INSERT on carteeditura 
FOR EACH ROW 
	BEGIN
		IF ((select count(Carte_idCarte) from carteeditura,editura where new.Editura_idEditura=idEditura) = 0) THEN
			begin	
				insert into carteeditura(Carte_idCarte,Editura_idEditura)
				values (new.Carte_idCarte, new.Editura_idEditura);
			end;
		else
			begin
				update editura
				set NrCarti=NrCarti+1 
				where idEditura=editura_ideditura;
			end;
		END IF;
	END;
DELIMITER ;