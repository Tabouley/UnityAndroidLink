# Présentation du projet UnityAndroidLink!

Le projet est composée d'un projet **Unity**  et d'un projet **Android Studio ( Java )** contenant 2 modules. Un module pour des requêtes HTTP asynchrones et un autre afin de faire vibrer l'appareil. 
Le projet Unity se trouve dans le dossier **LumeenProject** et le projet Android Studio dans le dossier **LumeenProjetJava**
 
## Projet Unity
Le projet Unity est composée d'une scène simple composée de 3 boutons et d'un *Text*.
Les boutons ont différentes fonctions:
- Envoyer une notification **Toast**
- Faire vibrer l'appareil
- Afficher une blague dans le *Text* récupérer via une requête HTTP asynchrone.
Ainsi qu'une classe gérant les *callbacks* provenant de **Java**.
### Notifications Toast
Les notifications Toast sont gérées directement dans un script Unity qui se sert de la librairie **Widget** de Java.
On commence par récupérer le contexte de l'application :  
```C#
AndroidJavaObject appContext = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
```
Puis on récupère la classe **Toast** afin de créer un *Object*, initialisé en pensant à convertir notre message en *Java String* , et ensuite l'afficher.
```C#
//Fetch Widget Toast class
AndroidJavaClass ToastClass = new AndroidJavaClass("android.widget.Toast");
//Convert string msg to Java String
AndroidJavaObject msgString = new AndroidJavaObject("java.lang.String", msg);
//Create the toast notification
AndroidJavaObject toastObject = ToastClass.CallStatic<AndroidJavaObject>("makeText", appContext, msgString, ToastClass.GetStatic<int>("LENGTH_SHORT")); 
// Show the toast notification
toastObject.Call("show");
```

### Vibrations de l'appareil

On utilise simplement ici un appel de la fonction issue du plugin **Vibration**.
```C#
AndroidJavaObject vibrate = new AndroidJavaObject("com.lumeenproject.vibrationmodule.VibrationHandler");

vibrate.Call("vibrate", currentActivity.Call<AndroidJavaObject>("getApplicationContext"));
```

 ### Joke HTTP Request
 On appelle ici une fonction issue du plugin **HTTPRequest** en lui fournissant une instance de notre gestionnaire de *callback* en paramètre.
 ```C#
 AndroidJavaObject HTTPRequestHandler = new AndroidJavaObject("com.lumeenproject.httphandler.HTTPRequestHandler");
 
HTTPRequestHandler.Call("requestJoke",new AndroidCallback(this));
 ```


### Gestionnaire de *callback*
Le gestionnaire est une classe héritant de AndroidJavaProxy qui implémente l'interface **HTTPRequestCallback** qui est en Java.

Le *callback* ne s'effectuant pas sur le thread principal de Unity , il est nécessaire de fournir notre instance principale héritant de  **MonoBehavior** au constructeur afin de pouvoir, par la suite, modifier le *Text* de l'UI.

Le *callback* se présente sous deux formes : **Succès** ou **Échec**

#### Succès :
En cas de succès , notre gestionnaire reçoit les informations reçues suite à la requête **HTTP** en format **JSON**. Les informations sont ensuite désérialisée dans une instance de classe puis en modifiant la valeur d'un attribut de notre instance principale , l'UI sera mise à jour à la prochaine *frame*.
```C#
public void onSuccess(string res)
{
	Joke joke = Joke.CreateFromJSON(res);
	linker.jokeString = joke.setup + "\n...\n" + joke.punchline;
}
```


#### Échec :

En cas d'échec , notre gestionnaire reçoit le message d'erreur qui sera affiché là où la blague est censée apparaître dans notre UI.
```C#
public void onError(string errorMsg)
{
	linker.jokeString = "ERROR : " + errorMsg;
}
```

## Projet Android Studio
### Module Vibration

Ce module est appelé dans Unity lors  de l’évent OnClick() sur le bouton associé.
On récupère tout d'abord le Vibrator Service :
```Java
Vibrator vibrator = (Vibrator) context.getSystemService(Context.VIBRATOR_SERVICE);
```
La méthode Vibrate(long ms) étant obsolète depuis la Version 26 de l'Android SDK, remplacée par la méthode Vibrate(ms , amplitude ),  il est nécessaire de tester la version utilisée.
```Java
if(Build.VERSION.SDK_INT>=26){
vibrator.vibrate(VibrationEffect.createOneShot(300,VibrationEffect.DEFAULT_AMPLITUDE));
} else
{
	vibrator.vibrate(300);
}
```

### Module HTTP Request

Le module est composée de 2 classes et d'une interface.
- **HTTPRequestCallback** : Interface implémentée en C# permettant d'assurer le *callback* vers Unity.
- **HTTPAsyncTask** : Classe héritant de **AsyncTask** permettant d'effectuer la requête HTTP en asynchrone afin de ne pas bloquer le thread principal.
- **HTTPRequestHandler** : Classe faisant le lien entre Unity et **HTTPAsyncTask**

## API Utilisée

L'API appartient à [@15DKatz]([https://github.com/15Dkatz) et celle-ci se nomme **Official Joke API** , malgré ces blagues douteuses et en anglais , vous pouvez retrouver cette api sur le lien github suivant :
[https://github.com/15Dkatz/official_joke_api](https://github.com/15Dkatz/official_joke_api)






##
![alt text](https://cdn4.iconfinder.com/data/icons/logos-and-brands/512/144_Gitlab_logo_logos-256.png#center)
