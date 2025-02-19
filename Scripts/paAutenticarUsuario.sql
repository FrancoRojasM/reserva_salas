USE [BDCASILLA]
GO
/****** Object:  StoredProcedure [Seguridad].[paAutenticarUsuario]    Script Date: 18/06/2024 16:27:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
		ALTER PROCEDURE [Seguridad].[paAutenticarUsuario]
			@pLogueo VARCHAR(30),
			@pClave VARCHAR(4000)	
		AS
		BEGIN TRY	
			SELECT  
			U.IdCatalogoTipoUsuario,
			U.Bloqueado,
			coalesce(U.IdPersona,0)IdPersona,
			U.IdUsuario,			
			U.Logueo,
			coalesce(P.NumeroDocumento,'')NumeroDocumento,	
			P.NombreCompleto,
			CASE WHEN coalesce(u.RutaArchivoFoto,'')='' THEN CASE WHEN P.Sexo=0 THEN 'sinfotoH.jpg' ELSE 'sinfotoM.jpg' END ELSE u.RutaArchivoFoto END  RutaArchivoFoto,
			coalesce(u.Email,'')Email,
				coalesce(U.EsInstitucion,0)	EsInstitucion,
			coalesce(P.NumeroCelular,'')NumeroCelular,
			coalesce(P.TelefonoFijo,'')TelefonoFijo,
			coalesce(P.Direccion,'')Direccion,
			coalesce(P.IdCatalogoTipoDocumentoPersonal,0)IdCatalogoTipoDocumentoPersonal,
			A.IdAdministrado
			FROM Seguridad.Usuario U 
			INNER JOIN [Casilla].[Administrado] A ON A.IdPersona=U.IdPersona AND A.IdUsuario=U.IdUsuario
			INNER JOIN General.Persona P ON P.IdPersona= U.IdPersona
			where U.Logueo=@pLogueo and U.Clave=@pClave and U.EstadoAuditoria=1
		END TRY
		BEGIN CATCH
			DECLARE @ERROR_NUMBER INT, @ERROR_SEVERITY INT,@ERROR_STATE INT,@ERROR_LINE INT,@ERROR_PROCEDURE VARCHAR(MAX)	,@ERROR_MESSAGE VARCHAR(MAX)	
			SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE=ERROR_PROCEDURE(),@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
			EXEC Seguridad.paGuardarErroresEnLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE                
		END CATCH
