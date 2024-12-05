using System;
using System.ServiceModel;
//using System.ServiceModel.Channels;

namespace MatrixUtilitarios
{
	public class InvocadorServicio<T>
	{
		public const string _variableEntornoServidor = "TEST_WCF";

	
		private string ObtieneServidor()
		{
			string text = Environment.GetEnvironmentVariable(_variableEntornoServidor, EnvironmentVariableTarget.Machine);
			if (text == null)
			{
				text = "dldswcf";
			}
			if (text == "")
			{
				text = "dldswcf";
			}
			return text;
		}
	}
}