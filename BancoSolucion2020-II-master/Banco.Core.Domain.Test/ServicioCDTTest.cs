using NUnit.Framework;
using System;

namespace Banco.Core.Domain.Test
{
    public class ServioCDTTest
    {
        [SetUp]
        public void Setup()
        {
        }

        //Escenario: primer consigna minimo 1 millon
        //H7: Como Usuario quiero realizar consignar mi dinero a mi CDT para ahorrar el dinero sin teneracceso al de acuerdo al término definido.
        //Criterio de Aceptación:
        //7.1 El valor de consignación inicial debe ser de mínimo 1 millón de pesos. 
        //Ejemplo
        ////Dado El cliente tiene una CuentaCDT
        ////Número 10001 , ciudad Valledupar , fecha acctual                            
        //Cuando Va a crear / consignar
        //Entonces El sistema presentará el mensaje. "El valor de consignación inicial debe ser de mínimo 1 millón de pesos."
        [Test]
        public void ValorMinimo1MillonTest()
        {
            //Preparar
            var cuentaCDT = new CuentaCDT( "10001", "Valledupar",decimal.Parse(0.06.ToString()),"SEMESTRAL");
            //Acción
            var resultado = cuentaCDT.Consignar(900000,"Valledupar","10-2020");
            //Verificación
            Assert.AreEqual("El valor de consignación inicial debe ser de mínimo 1 millón de pesos.", resultado);
        }

        //Escenario: segunda consignacion invalida
        //H7: Como Usuario quiero realizar consignar mi dinero a mi CDT para ahorrar el dinero sin teneracceso al de acuerdo al término definido.
        //Criterio de Aceptación:
        //7.2  Sólo se podrá realizar una sola consignación. 
        //Ejemplo
        ////Dado El cliente tiene una CuentaCDT
        ////Número 10001 , ciudad Valledupar , fecha actual                            
        //Cuando Va a consignar por segunda vez en la fecha
        //Entonces El sistema presentará el mensaje. "Ya no puedes consignar hasta el termino definido
        [Test]
        public void SoloUnaConsignacionTest()
        {
            //Preparar
            var cuentaCDT = new CuentaCDT("10001", "Valledupar", decimal.Parse(0.06.ToString()), "ANUAL");
            //Acción
            cuentaCDT.Consignar(10000000, "Valledupar", "10-2020");
            var resultado = cuentaCDT.Consignar(10000000, "Valledupar", "10-2020");
            //Verificación
            Assert.AreEqual("Ya no puedes consignar hasta el termino definido", resultado);
        }

        //Escenario: Retirar cuando se llegue al termino
        //H7: Como Usuario quiero realizar consignar mi dinero a mi CDT para ahorrar el dinero sin teneracceso al de acuerdo al término definido.
        //Criterio de Aceptación:
        //8.1 Los retiros sólo se podrán realizar una vez haya finalizado el término del CDT.   
        //Ejemplo
        ////Dado El cliente tiene una CuentaCDT
        ////Número 10001 , ciudad Valledupar , fecha actual                            
        //Cuando Va a consignar por segunda vez en la fecha
        //Entonces El sistema presentará el mensaje. "Ya no puedes consignar hasta el termino definido
        [Test]
        public void RetiroInvalidoTerminoTest()
        {
            //Preparar
            var cuentaCDT = new CuentaCDT("10001", "Valledupar", decimal.Parse(0.06.ToString()),"TRIMESTRAL");
            //Acción
            cuentaCDT.Consignar(10000000, "Valledupar", "10-2020");
            var resultado = cuentaCDT.Retirar(900000, "11-2020");
            //Verificación
            Assert.AreEqual("Ya no puedes consignar hasta el termino definido", resultado);
        }


        //Escenario: valor a retirar incorrecto
        //H1: Como Usuario quiero realizar consignaciones (abonos) a una Tarjeta Crédito para abonar al saldo del servicio.
        //Criterio de Aceptación:
        // El valor del avance debe ser mayor a 0.
        //Ejemplo
        ////Dado El cliente tiene una tarjeta de credito
        ////Número 10001, Nombre “Cuenta ejemplo”, cupo 200.000 , ciudad Valledupar                              
        //Cuando Va a retirar 0
        //Entonces El sistema presentará el mensaje. “avance no aceptado, por favor verifique el valor a retirar”
        [Test]
        public void AvanceMayorCeroTest()
        {
            //Preparar
            var tarjetaCredito = new TarjetaCredito( numero: "10001", nombre:"Cuenta ejemplo", ciudad:"Valledupar", cupo: 200000);
            //Acción
            var resultado= tarjetaCredito.Retirar(0, DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificación
            Assert.AreEqual("avance no valido, verifique el valor a retirar", resultado);
        }
        
        //Escenario: valor a retirar incorrecto
        //H1: Como Usuario quiero realizar consignaciones (abonos) a una Tarjeta Crédito para abonar al saldo del servicio.
        //Criterio de Aceptación:
        // Un avance no podrá ser mayor al valor disponible del cupo.
        //Ejemplo
        ////Dado El cliente tiene una tarjeta de credito
        ////Número 10001, Nombre “Cuenta Ejemplo”, cupo 200.000 , ciudad Valledupar                               
        //Cuando Va a retirar 250.000
        //Entonces El sistema presentará el mensaje. “avance no aceptado, el valor a retirar es mayor al cupo disponible en su tarjeta”
        [Test]
        public void AvanceMayorCupoTest()
        {
            //Preparar
            var tarjetaCredito = new TarjetaCredito( numero: "10001", nombre:"Cuenta Ejemplo", ciudad:"Valledupar", cupo: 200000);
            //Acción
            var resultado= tarjetaCredito.Retirar(250000, DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificación
            Assert.AreEqual("avance no valido, el valor a retirar es mayor al cupo disponible en su tarjeta", resultado);
        }
        
        //Escenario: valor a abonar incorrecto
        //H1: Usuario quiero realizar consignaciones (abonos) a una Tarjeta Crédito para abonar al saldo del servicio.
        //Criterio de Aceptación:
        // El valor a abono no puede ser menor o igual a 0
        //Ejemplo
        ////Dado El cliente tiene una tarjeta de credito
        ////Número 10001, Nombre “Cuenta Ejemplo”, saldo 200.000 cupo 100.000, ciudad Valledupar                             
        //Cuando Va a consignar 0
        //Entonces El sistema presentará el mensaje. “Valor del abono no aceptado, por favor verifique el valor a abonar”
        [Test]
        public void AbonoValorMenorCeroTest()
        {
            //Preparar
            var tarjetaCredito = new TarjetaCredito( numero: "10001", nombre:"Cuenta Ejemplo", ciudad:"Valledupar", cupo: 300000);
            //Acción
            tarjetaCredito.Retirar(200000, DateTime.Now.Month + "-" + DateTime.Now.Year);
            var resultado= tarjetaCredito.Consignar(0, "valledupar", DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificación
            Assert.AreEqual("Valor del abono no valido", resultado);
        }
        
        //Escenario: valor a abonar incorrecto
        //H1: Usuario quiero realizar consignaciones (abonos) a una Tarjeta Crédito para abonar al saldo del servicio.
        //Criterio de Aceptación:
        // El abono podrá ser máximo el valor del saldo de la tarjeta de crédito. 
        //Ejemplo
        ////Dado El cliente tiene una tarjeta de credito
        ///Número 10001, Nombre “Cuenta Ejemplo”, saldo 200.000 cupo 100.000, ciudad Valledupar                             
        //Cuando Va a consignar 250000
        //Entonces El sistema presentará el mensaje. “el valor a abonar debe ser igual o menor al valor de la deuda actual”
        [Test]
        public void ValorAvanceMayorTest()
        {
            //Preparar
            var tarjetaCredito = new TarjetaCredito( numero: "10001", nombre:"Cuenta Ejemplo", ciudad:"Valledupar", cupo: 300000);
            //Acción
            tarjetaCredito.Retirar(200000, DateTime.Now.Month + "-" + DateTime.Now.Year);
            var resultado= tarjetaCredito.Consignar(250000, "valledupar", DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificación
            Assert.AreEqual("el valor del abono debe ser igual o menor al de la deuda actual", resultado);
        }
        
        //Escenario: valor a abonar correcto
        //H1: Usuario quiero realizar consignaciones (abonos) a una Tarjeta Crédito para abonar al saldo del servicio.
        //Criterio de Aceptación:
        // Al realizar un abono el cupo disponible aumentará con el mismo valor que el valor del abono y reducirá de manera equivalente el saldo.
        //Ejemplo
        ////Dado El cliente tiene una tarjeta de credito
        ////Número 10001, Nombre “Cuenta Ejemplo”, saldo 200.000 cupo 100.000, ciudad Valledupar                             
        //Cuando Va a consignar 100000
        //Entonces El sistema presentará el mensaje. “el valor a abonar debe ser igual o menor al valor de la deuda actual”
        [Test]
        public void AbonoValidoTest()
        {
            //Preparar
            var tarjetaCredito = new TarjetaCredito( numero: "10001", nombre:"Cuenta Ejemplo", ciudad:"Valledupar", cupo: 300000);
            //Acción
            tarjetaCredito.Retirar(200000, DateTime.Now.Month + "-" + DateTime.Now.Year);
            var resultado= tarjetaCredito.Consignar(100000, "valledupar", DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificación
            Assert.AreEqual("Abono valido. Su Nuevo Cupo es de $200,000.00 pesos m/c", resultado);
        }
    }
}