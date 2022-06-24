create database dbAutorizacao;

use dbAutorizacao;

create table tblUsuario(
UsuarioID int primary key auto_increment,
UsuNome varchar(100) not null unique,
Login varchar(50) not null unique,
Senha varchar (100) not null
);

-- Delimitador
delimiter $$
-- Criando um método no banco de dados
create procedure spInsertUsuarios(vUsuNome varchar(100),vLogin varchar(50),vSenha varchar(100))
Begin

insert into tblUsuario(UsuNome,Login,Senha)
			values(vUsuNome,vLogin,vSenha);
           
End 
$$

delimiter $$
create procedure spSelectLogin(vLogin varchar(50)) 
Begin

select Login from tblUsuario where Login = vLogin;
           
End
$$

delimiter $$
create procedure spSelectUsuarios(vLogin varchar(50)) 
Begin

select * from tblUsuario where Login = vLogin;
           
End
$$

delimiter $$
create procedure spUpdateSenha(vLogin varchar(50), vSenha varchar(100)) 
Begin

update tblUsuario set Senha = vSenha where Login = vLogin;
           
End
$$

delimiter $$
create procedure spDeleteUsuario(vUsuarioID int) 
Begin

DELETE FROM tblUsuario where UsuarioID = vUsuarioID;
           
End
$$

-- drop procedure UpdateSenha;

delimiter $$
create procedure spListarUsuarios() 
Begin

select * from tblUsuario;
           
End
$$

call spListarUsuarios;
call spDeleteUsuario('4');


Call spInsertUsuarios('Astrogildo','Porquinho','12345678');
Call spSelectLogin('Porquinho');
Call spSelectUsuarios('Porquinho');
Call spUpdateSenha('Porquinho','87654321');

select * from tblUsuario;

-- Truncate tblUsuario;