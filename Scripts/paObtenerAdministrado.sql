USE [BDCASILLA]
GO
/****** Object:  StoredProcedure [Casilla].[paObtenerAdministrado]    Script Date: 18/06/2024 16:32:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
		ALTER PROCEDURE [Casilla].[paObtenerAdministrado]
		@pIdAdministrado INT,
		@pIdUsuarioAuditoria int = NULL	
		AS
		BEGIN
		BEGIN TRY

			SELECT									
			COALESCE(A.IdPersona,0)IdPersona,	
			COALESCE(P1.NombreCompleto,'') NombreCompletoPersona,													
			COALESCE(A.IdUsuario,0)IdUsuario,	
			COALESCE(A.EmailNotificacion,'')EmailNotificacion,	
			COALESCE(A.NumeroCelularNotificacion,'')NumeroCelularNotificacion,	
			COALESCE(A.AsientoElectronico,'')AsientoElectronico,	
			COALESCE(A.PartidaElectronica,'')PartidaElectronica,	
			A.Activo,
			A.IdAdministrado
			FROM Casilla.Administrado A 
			INNER JOIN General.Persona P1 ON P1.IdPersona =A.IdPersona
			WHERE A.EstadoAuditoria=1 and A.IdAdministrado=@pIdAdministrado


		END TRY
			BEGIN CATCH					
				DECLARE @ERROR_NUMBER INT
				DECLARE @ERROR_SEVERITY INT
				DECLARE @ERROR_STATE INT
				DECLARE @ERROR_PROCEDURE VARCHAR(MAX)
				DECLARE @ERROR_LINE INT
				DECLARE @ERROR_MESSAGE VARCHAR(MAX)
				SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Casilla.paObtenerAdministrado',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
				EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE ,@pIdUsuarioAuditoria               
			END CATCH
		END
