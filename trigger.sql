DELIMITER //

create TRIGGER creste_nr_carti_editura AFTER INSERT on carteeditura 
FOR EACH ROW 
	BEGIN
		IF (select count(Carte_idCarte) from carteeditura,editura where new.Editura_idEditura=idEditura) = 0 THEN
			begin	
				insert into carteeditura(Carte_idCarte,Editura_idEditura)
				values (new.Carte_idCarte, new.Editura_idEditura);
			end;
		else
			begin
				update editura,carteeditura
				set NrCarti=NrCarti+1 
				where idEditura=new.editura_ideditura;
			end;
		END IF;
	END//
DELIMITER ;


delimiter $$
create TRIGGER scade_nr_carti_editura before delete ON carteeditura
for each row
	begin
		update carteeditura,carte,editura set editura.nrcarti=editura.nrcarti-1
		where old.editura_ideditura=ideditura;
	END $$ 
delimiter ;

delimiter //
create trigger introduce_in_istoric after insert on imprumuturi
for each row
	begin
	insert into istoricimprumuturi (idUtilizator,idCarte)
	values (new.idUtilizator,new.idCarte);
	end //
delimiter ;