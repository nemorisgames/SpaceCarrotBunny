
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using Prime31;

public class IAPNemoris : MonoBehaviour
{ 
	public bool compraTerminada=true;
#if UNITY_IPHONE || UNITY_ANDROID
	/*
	public bool isTest = true;
	public List<IAPProduct> _productos;

	public int[] montosMonedas;
	public storeItem[] productos;
	public string[] androidSkus = new string[] { "coinpack1", "coinpack2", "coinpack3", "coinpack4", "coinpack5" };
	public string[] iosProductIds = new string[] { "coinpack1", "coinpack2", "coinpack3", "coinpack4", "coinpack5" };
	public GameObject imagenLoading;

	string receiptActual;

	public bool debug = true;
	string retorno = "";

	void Start(){
		if(imagenLoading != null)imagenLoading.SetActive (false);
		var key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAqz/sBX+z0mLmU7E1c9xmZI74vR5+iQNNCQJrvIWpglWIXqktZE3U1E9DPOvRgDXDGK5F2a6vpl9JE6meTN18VS1TonMCQmEXTs7gNAIhHGmwGLLcsarI/aRAkIm4AsJnYZ50FxcD4++jCcgnqO1erInszMY37lyNhxwWP/0gv3IglL323zGOE5uaEnDg0aeRvqIw5AQajdJz1HLJzKGPWyq2N8WYvphTtdv3F1s5syEnUi+eT7CJMLGmP9EjT0heLGijAztLKVI9joIYLkwyX+iitTCJbs72PHHHy/KA0r3C/14jif0Tc+zNJoUaBXeXJRdYKGv0+osXULBXkVfy7wIDAQAB";
		#if UNITY_ANDROID
		key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAqz/sBX+z0mLmU7E1c9xmZI74vR5+iQNNCQJrvIWpglWIXqktZE3U1E9DPOvRgDXDGK5F2a6vpl9JE6meTN18VS1TonMCQmEXTs7gNAIhHGmwGLLcsarI/aRAkIm4AsJnYZ50FxcD4++jCcgnqO1erInszMY37lyNhxwWP/0gv3IglL323zGOE5uaEnDg0aeRvqIw5AQajdJz1HLJzKGPWyq2N8WYvphTtdv3F1s5syEnUi+eT7CJMLGmP9EjT0heLGijAztLKVI9joIYLkwyX+iitTCJbs72PHHHy/KA0r3C/14jif0Tc+zNJoUaBXeXJRdYKGv0+osXULBXkVfy7wIDAQAB";
		#endif	
		#if UNITY_IPHONE
		//no estoy seguro de que esto sirva para ios
			key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAmffbbQPr/zqRjP3vkxr1601/eKsXm5kO2NzQge8m7PeUj5V+saeounyL34U8WoZ3BvCRKbw6DrRLs2DMoVuCLq7QtJggBHT/bBSHGczEXGIPjWpw6OQb24EWM0PaTRTH2x2mC/X6RwIKcPLJFmy68T38Eh0DXnF4jjiIoaD0W8AYLjLzv0WvbIfgtJlvmmwvI2/Kta1LRnW3/Ggi5jb9UmXZAUIBz8kQtSH5FUCmFOQHMzekfg8rQ4VO1nlWhnB58UPwsxWt/DNyDfqv2VMeA2+VJG0fkiMl/6vWA7+ianVTU3owXcvxJHseEDUVYo1wEKfhK7ErGB7sxDJx5wHXAwIDAQAB";
		#endif	
		IAP.init (key);
		#if UNITY_IPHONE
			cargarProductos();
		#endif	
		DontDestroyOnLoad (gameObject);
	}

	public void OnEnable(){
		#if UNITY_IPHONE
		StoreKitManager.purchaseSuccessfulEvent += purchase_sucessful;
		#endif
		#if UNITY_ANDROID
		GoogleIABManager.billingSupportedEvent += billingSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
		#endif	
	}

	public void OnDisable(){
		#if UNITY_IPHONE
		StoreKitManager.purchaseSuccessfulEvent -= purchase_sucessful;
		#endif
		#if UNITY_ANDROID
		GoogleIABManager.billingSupportedEvent -= billingSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
		#endif	
	}
	
	//esto se ejecuta solo en android
	void billingSupportedEvent()
	{
		retorno = "billing supported";
		cargarProductos();
	}

	#if UNITY_ANDROID
	void queryInventorySucceededEvent( List<GooglePurchase> purchases, List<GoogleSkuInfo> skus )
	{
		retorno = ( string.Format( "queryInventorySucceededEvent. total purchases: {0}, total skus: {1}", purchases.Count, skus.Count ) );
		Prime31.Utils.logObject( purchases );
		Prime31.Utils.logObject( skus );
	}
	#endif

	public void restoreTransactions(){
		IAP.restoreCompletedTransactions (_restaurar =>
		                                  {
			print (_restaurar);
		});
	}
	
	void queryInventoryFailedEvent( string error )
	{
		retorno += "queryInventoryFailedEvent: " + error ;
	}

	#if UNITY_IPHONE
	public void purchase_sucessful(StoreKitTransaction transaction){
		receiptActual = transaction.base64EncodedTransactionReceipt;
		StartCoroutine("validarReceipt", transaction.productIdentifier);
	}
	#endif
	public void cargarProductos(){
		retorno = "cargando productos";
		IAP.requestProductData( iosProductIds, androidSkus, _productos =>
		{
			Debug.Log( "Product list received " + _productos.Count );
			retorno = "Product list received " + _productos.Count;
			Utils.logObject( _productos );
			print ("productos sumados");
			retorno += "productos sumados";
		});

	}

	public void comprar(int id){
		compraTerminada = false;
		if(imagenLoading != null)imagenLoading.SetActive (true);
		//IAP.receipt = "";
		receiptActual = "";
		retorno = "comprar";
		#if UNITY_ANDROID
		var productId = androidSkus[id];
		#elif UNITY_IPHONE
		var productId = iosProductIds[id];
		#endif
		IAP.purchaseConsumableProduct( productId, (didSucceed, error) =>
		{
			Debug.Log( "purchasing product " + productId + " result: " + didSucceed );
			retorno = "purchasing product " + productId + " result: " + didSucceed;
			if(didSucceed){
				#if UNITY_ANDROID
				compraTerminada = true;
				retorno = "compra exitosa";

				//retorno += montosMonedas[obtenerIndiceMonto(productId)] + " " + obtenerIndiceMonto(productId) + " " + productId;
				//gameObject.SendMessage("compraExitosa", montosMonedas[obtenerIndiceMonto(productId)], SendMessageOptions.DontRequireReceiver);
				if(imagenLoading != null)imagenLoading.SetActive (false);
				#elif UNITY_IPHONE
				//VALIDAR RECEIPT
				//StartCoroutine("validarReceipt", productId);
				#endif
				//PlayerPrefs.SetInt("activateAds", 0);
				//estas dos no son necesarias porque se hace en el validar receipt, pero igual las dejo por siaca
				PlayerPrefs.SetInt ("activateAdsAdBuddiz", 0);
				PlayerPrefs.SetInt ("activateAdsHeyDay", 0);
				GameObject.FindWithTag("MainCamera").SendMessage("compraExitosa");
			}
			else{
				//gameObject.SendMessage("compraFallida", 0, SendMessageOptions.DontRequireReceiver);
				GameObject.FindWithTag("MainCamera").SendMessage("compraFallida");
				if(imagenLoading != null)imagenLoading.SetActive (false);
				compraTerminada = true;
			}

		});
	}

	public IEnumerator validarReceipt(string productId){
		while (receiptActual == ""){ //IAP.receipt == "") {
			yield return new WaitForSeconds(0.5f);
		}

		WWWForm form = new WWWForm();
		form.AddField ("receipt", receiptActual); //IAP.receipt );
		form.AddField( "sandbox", "" + isTest );
		var download = new WWW( "http://www.nemorisgames.com/medusa/funciones.php?operacion=4", form);
		yield return download;
		if(download.error != null) {
			print( "Error downloading: " + download.error );
			retorno = "Error downloading: " + download.error;
			//mostrarError("Error de conexion");
			return false;
		} else {
			string retorno = download.text;
			print ("retorno receipt " + retorno);
			retorno = "retorno receipt " + retorno;
			if(retorno == ""){
				//error :(
				//mostrarError("Error de conexion");
				GameObject.FindWithTag("MainCamera").SendMessage("compraFallida");
			}
			else{
				//exito!
				//escribe en consola lo que recibe. no se esta parseando
				//JSONObject j = new JSONObject(retorno);
				//accessData(j);

				PlayerPrefs.SetInt ("activateAdsAdBuddiz", 0);
				PlayerPrefs.SetInt ("activateAdsHeyDay", 0);

				GameObject.FindWithTag("MainCamera").SendMessage("compraExitosa");

				//gameObject.SendMessage("compraExitosa", montosMonedas[obtenerIndiceMonto(productId)], SendMessageOptions.DontRequireReceiver);

				//Application.LoadLevel(Application.loadedLevelName);
			}
		}	
		if(imagenLoading != null)imagenLoading.SetActive (false);
		compraTerminada = true;
	}
	
	int obtenerIndiceMonto(string id){
		for(int i = 0; i < productos.Length; i++){
			if(productos[i].id == id) return i;	
		}
		return -1;
	}

	void OnGUI(){
		if (!debug)
			return;
		GUI.Box (new Rect (0, 0, Screen.width, 200), "ret: " + retorno);
		//if(GUI.Button(new Rect(0, 300, 250, 60), "comprar")){
		//	comprar(0);
		//}
	}
*/
#endif

}

[System.Serializable]
public class storeItem{
	public string nombre;
	public int monedas;
	public string id;
	public string precio;
	public string currency;
}
