using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace WcfService1
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract] List<Pais> ListadoPais();
        [OperationContract] List<Cliente> ListadoCliente();
        [OperationContract] string AgregarCliente(Cliente reg);
        [OperationContract] string ActualizarCliente(Cliente reg);
        [OperationContract] string EliminarCliente(string id);

    }


    
   [DataContract] public class Cliente
    {
        [DataMember] public string idcliente { get; set; }
        [DataMember] public string nombre { get; set; }
        [DataMember] public string direccion { get; set; }
        [DataMember] public string idpais { get; set; }
        [DataMember] public string telefono { get; set; }
    }

    [DataContract] public class Pais
    {
        [DataMember] public string idpais { get; set; }
        [DataMember] public string nombrePais { get; set; }
    }
}
