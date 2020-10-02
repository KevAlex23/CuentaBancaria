using NUnit.Framework;
using System;

namespace Banco.Core.Domain.Test
{
    public class TestsCorriente
    {
        [SetUp]
        public void Setup()
        {
        }

        //Escenario: Valor de consignación negativo o cero 
        //H3: Como Usuario quiero realizar consignaciones a una cuenta corriente para salvaguardar el dinero
        //Criterio de Aceptación:
        //3.0
        //Ejemplo
        //Dado El cliente tiene una cuenta de ahorro                                       //A =>Arrange /Preparación
        //Número 10001, Nombre “Cuenta ejemplo”, Saldo de 0 , ciudad Valledupar, cupo de 500                            
        //Cuando Va a consignar un valor menor o igual a cero  (0)                            //A =>Act = Acción
        //Entonces El sistema presentará el mensaje. "El valor a consignar es incorrecto"  //A => Assert => Validación
        [Test]
        public void NoPuedeConsignacionValorNegativoTest()
        {
            //Preparar
            var cuentaCorriente = new CuentaCorriente(cupo: 500, numero: "10001", nombre:"Cuenta Ejemplo", ciudad:"Valledupar");
            //Acción
            var resultado=cuentaCorriente.Consignar(0,"", DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificación
            Assert.AreEqual("El valor a consignar es incorrecto", resultado);
        }


        //Escenario: Valor de consignación valido
        //H3: Como Usuario quiero realizar consignaciones a una cuenta corriente para salvaguardar el dinero
        //Criterio de Aceptación:
        //3.2 El valor consignado debe ser adicionado al saldo de la cuenta.
        //Ejemplo
        //Dado El cliente tiene una cuenta de ahorro                                       //A =>Arrange /Preparación
        //Número 10001, Nombre “Cuenta Ejemplo”, cupo de 500 , ciudad Valledupar                               
        //Cuando Va a consignar un valor menor o igual a cero  (0)                            //A =>Act = Acción
        //Entonces El sistema presentará el mensaje. "Su Nuevo Saldo es de $600,000.00 pesos m/c"  //A => Assert => Validación
        [Test]
        public void ConsignacionInicialCorrectaTest()
        {
            //Preparar
            var cuentaCorriente = new CuentaCorriente(cupo: 500, numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Valledupar");
            //Acción
            var resultado = cuentaCorriente.Consignar(100000, "", DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificación
            Assert.AreEqual("Su Nuevo Saldo es de $100,500.00 pesos m/c", resultado);
        }

        //Escenario: Valor de consignación negativo o cero 
        //H3: Como Usuario quiero realizar consignaciones a una cuenta corriente para salvaguardar el dinero
        //Criterio de Aceptación:
        //3.1 La consignación inicial debe ser de mínimo 100 mil pesos. 
        //Ejemplo
        //Dado El cliente tiene una cuenta de ahorro                                       //A =>Arrange /Preparación
        //Número 10001, Nombre “Cuenta ejemplo”, Saldo de 0 , ciudad Valledupar, cupo de 500                            
        //Cuando Va a consignar un valor menor o igual a cero  (0)                            //A =>Act = Acción
        //Entonces El sistema presentará el mensaje. "El valor a consignar es incorrecto"  //A => Assert => Validación
        [Test]
        public void NoPuedeConsignarMenosDe100MilTest()
        {
            //Preparar
            var cuentaCorriente = new CuentaCorriente(cupo: 500, numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Valledupar");
            //Acción
            var resultado = cuentaCorriente.Consignar(99999,"", DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificación
            Assert.AreEqual("El valor mínimo de la primera consignación debe ser de $100.000 mil pesos. Su nuevo saldo es $0 pesos", resultado);
        }
        ///////////////////////////////////////
        //Pruebas Retiros
        //////////////////////////////////////

        [Test]
        public void NoPuedeRetitrarMenosDeLoQueHayTest()
        {
            //Preparar
            var cuentaCorriente = new CuentaCorriente(cupo: 500, numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Valledupar");
            //Acción
            cuentaCorriente.Consignar(200000, "", DateTime.Now.Month + "-" + DateTime.Now.Year);
            var retirar = cuentaCorriente.Retirar(300000, DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificación
            Assert.AreEqual("Saldo insuficiente. Su saldo es de $200,500.00 pesos m/c", retirar);
        }

        [Test]
        public void SaldoMinimoMayorCupoTest()
        {
            //Preparar
            var cuentaCorriente = new CuentaCorriente(cupo: 500, numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Valledupar");
            //Acción
            cuentaCorriente.Consignar(500000, "", DateTime.Now.Month + "-" + DateTime.Now.Year);
            var retirar = cuentaCorriente.Retirar(500100, DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificación
            Assert.AreEqual("Saldo insuficiente. Su saldo es de $500,500.00 pesos m/c", retirar);
        }
       
        [Test]
        public void ValorRetirarDescontarDelSaldoTest()
        {
            //Preparar
            var cuentaCorriente = new CuentaCorriente(cupo: 500, numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Valledupar");
            //Acción
            cuentaCorriente.Consignar(500000,"", DateTime.Now.Month + "-" + DateTime.Now.Year);
            var retirar = cuentaCorriente.Retirar(300000, DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificación
            Assert.AreEqual("Saldo retirado. Su Nuevo Saldo es de $199,300.00 pesos", retirar);
        }



    }
}