using UnityEngine;
using System.Collections;

public class EnviromentData : MonoBehaviour
{
	//TODO:
	//Esta clase va a contener toda la información necesaria 
	//para que el aldeano pueda decidir que hacer (acceden a esta info haciendo algo como
	//EnviromentData.Instance.foodQty).
	//Por ejemplo acá pueden poner:
	//1. Cuanta madera tiene (la necesita para construir)
	//2. Cuanta comida tiene (la necesita para vivir)
	//3. Estado del clima (si llueve no debería salir de su casa porque se enferma)
	//4. Momento del día (de noche es peligroso salir)	

	//La siguiente region les va a servir para
	//acceder al aldeano desde sus nodos 'Accion' (deberán heredar de los nodos 'Accion'		
	//y en el método Execute() hacer algo como 'EnviromentData.Instance.citizen.DoSomething()')
#region DONT TOUCH THIS
	public Citizen citizen;
	public static EnviromentData Instance { get; private set; }
	void Awake()
	{
		Instance = this;
	}
#endregion
}
