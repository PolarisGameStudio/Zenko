using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using VoxelBusters.NativePlugins;


// Placing the Purchaser class in the CompleteProject namespace allows it to interact with ScoreManager, 
// one of the existing Survival Shooter scripts.
namespace CompleteProject
{
    // Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
    public class Purchaser : MonoBehaviour, IStoreListener
    {
        public BillingProduct noAds;
        public BillingProduct[] products;
        private static IStoreController m_StoreController;          // The Unity Purchasing system.
        private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

        // Product identifiers for all products capable of being purchased: 
        // "convenience" general identifiers for use with Purchasing, and their store-specific identifier 
        // counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers 
        // also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

        // General product identifiers for the consumable, non-consumable, and subscription products.
        // Use these handles in the code to reference which product to purchase. Also use these values 
        // when defining the Product Identifiers on the store. Except, for illustration purposes, the 
        // kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
        // specific mapping to Unity Purchasing's AddProduct, below.
        public static string NO_ADS = "zenko.noads";
        public static Purchaser Instance;
 

        void Start()
        {
            if(Instance != null){
                Destroy(this.gameObject);
                return;
            }
            else{
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }


            // If we haven't set up the Unity Purchasing reference
            #if UNITY_ANDROID
            if (m_StoreController == null)
            {
                // Begin to configure our connection to Purchasing
                InitializePurchasing();
            }
            #endif

            #if UNITY_IOS
                products = NPSettings.Billing.Products;
                NPBinding.Billing.RequestForBillingProducts(products);
            #endif
        }

        private void OnEnable ()
        {
            Debug.Log("Added on enable shits");
            // Register for callbacks
            Billing.DidFinishRequestForBillingProductsEvent    += OnDidFinishProductsRequest;
            Billing.DidFinishProductPurchaseEvent            += OnDidFinishTransaction;

            // For receiving restored transactions.
            Billing.DidFinishRestoringPurchasesEvent        += OnDidFinishRestoringPurchases;

        }

        private void OnDisable ()
        {
            // Deregister for callbacks
            Billing.DidFinishRequestForBillingProductsEvent    -= OnDidFinishProductsRequest;
            Billing.DidFinishProductPurchaseEvent            -= OnDidFinishTransaction;
            Billing.DidFinishRestoringPurchasesEvent        -= OnDidFinishRestoringPurchases;        
        }
        private void OnDidFinishProductsRequest (BillingProduct[] _regProductsList, string _error)
        {
            // Hide activity indicator

            // Handle response
            if (_error != null)
            {        
                // Something went wrong
                Debug.Log("COULDNT LOAD PRODUCTS");
            }
            else 
            {    
                Debug.Log("LOADED PRODUCTS");
                products = _regProductsList;
                noAds = _regProductsList[0];

                //encontrar uno legit
                //noAdPrice.text = noAds.LocalizedPrice;
                int index = 0;
                NPBinding.Billing.RestorePurchases();
            }
        }

        private void OnDidFinishRestoringPurchases (BillingTransaction[] _transactions, string _error)
        {
            Debug.Log("Finished restoring purchases loaded");
            //Debug.Log(string.Format("Received restore purchases response. Error = {0}.", _error.GetPrintableString()));

            if (_transactions != null)
            {                
                Debug.Log("Count of transaction information received = " + _transactions.Length);

                foreach (BillingTransaction _currentTransaction in _transactions)
                {
                    Debug.Log("GOING WITH CURRENT RESTORE NAME " + _currentTransaction.ProductIdentifier);
                    Debug.Log(_currentTransaction.ProductIdentifier);
                    switch(_currentTransaction.ProductIdentifier){
                        case "noads":
                            //Debug.Log("RESTORING NO ADS");
                            RemoveAds();
                            break;
                    }
                }
            }
            if(_transactions == null){
                Debug.Log("RETURNING TRANSACTIONS NULL");
                Debug.Log("ERROR IS " + _error);
            }
        }

        private void OnDidFinishTransaction (BillingTransaction _transaction)
        {
            if (_transaction != null)
            {
                Debug.Log("NON NULL TRANSACTION");
                if (_transaction.VerificationState == eBillingTransactionVerificationState.SUCCESS)
                {
                    Debug.Log(_transaction.ProductIdentifier + "HAS SUCCESSFULLY BEEN SUCCESED");
                    if (_transaction.TransactionState == eBillingTransactionState.PURCHASED)
                    {
                        Debug.Log(_transaction.ProductIdentifier + "HAS SUCCESSFULLY BEEN PURCHASED");
                        switch(_transaction.ProductIdentifier){
                            case "noads":
                                RemoveAds();
                                break;

                        }
                        // Your code to handle purchased products
                    }
                }
            }
            else{
                Debug.Log("NULL TRANSACTION");
                Debug.Log(_transaction);
            }
        }
        
        public void InitializePurchasing() 
        {
            // If we have already connected to Purchasing ...
            if (IsInitialized())
            {
                // ... we are done here.
                return;
            }

            // Create a builder, first passing in a suite of Unity provided stores.
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());


            // Continue adding the non-consumable product.
            builder.AddProduct(NO_ADS, ProductType.NonConsumable);
 
            // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
            // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
            UnityPurchasing.Initialize(this, builder);
        }


        private bool IsInitialized()
        {
            // Only say we are initialized if both the Purchasing references are set.
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }


        public void BuyNoAds()
        {
            // Buy the non-consumable product using its general identifier. Expect a response either 
            // through ProcessPurchase or OnPurchaseFailed asynchronously.
            BuyProductID(NO_ADS);
        }

        public float PriceOfNoAds(){
            #if UNITY_ANDROID
            if(IsInitialized()){
                Product product = m_StoreController.products.WithID(NO_ADS);
                return (float)product.metadata.localizedPrice;
            }
            return 40f;
            #endif
            #if UNITY_IOS
            if(noAds!=null){
                Debug.Log("returning noads.price");
                return (float)noAds.Price;
            }
                
            else{
                Debug.Log("Returning 0.99");
                return 0.99f;
            }
            #endif
        }


        void BuyProductID(string productId)
        {
            #if UNITY_ANDROID
            // If Purchasing has been initialized ...
            if (IsInitialized())
            {
                // ... look up the Product reference with the general product identifier and the Purchasing 
                // system's products collection.
                Product product = m_StoreController.products.WithID(productId);

                // If the look up found a product for this device's store and that product is ready to be sold ... 
                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                    // asynchronously.
                    m_StoreController.InitiatePurchase(product);

                    //RemoveAds();
                }
                // Otherwise ...
                else
                {
                    // ... report the product look-up failure situation  
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            // Otherwise ...
            else
            {
                // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
                // retrying initiailization.
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
            #endif
            #if UNITY_IOS
            if (NPBinding.Billing.IsProductPurchased(noAds))
            {       
                // Show alert message that item is already purchased
                Debug.Log("ALREADY PURCHASED");
                RemoveAds();
                return;
            }
            else{
                // Call method to make purchase
                NPBinding.Billing.BuyProduct(noAds);
            }
            #endif
        }

        // Takes action to remove ads. 
        public void RemoveAds(){   
            Debug.Log("REMOVING ADS");
            PlayerPrefs.SetInt("AdFree", 1);
            LevelManager.adFree = true;
            PlayerPrefs.Save();
            PlayServices.instance.SaveLocal();
            PlayServices.instance.SaveData();
            if(GameObject.Find("BuyMenu")!=null)
                GameObject.Find("BuyMenu").SetActive(false);
            if(SceneLoading.menuState == "Potd"){
                SceneLoading.Instance.ClosePotdMode();
                SceneLoading.Instance.PuzzleOfTheDayMenu();   
            }
            PotdShortcut.Instance.AssignPotdShortcutAssets(PotdUnlocker.Instance.keysAvailable);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            // Purchasing has succeeded initializing. Collect our Purchasing references.
            Debug.Log("OnInitialized: PASS");

            // Overall Purchasing system, configured with products for this application.
            m_StoreController = controller;
            // Store specific subsystem, for accessing device-specific store features.
            m_StoreExtensionProvider = extensions;
        }


        public void OnInitializeFailed(InitializationFailureReason error)
        {
            // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        }


        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
        {

            // Or ... a non-consumable product has been purchased by this user.
            if (String.Equals(args.purchasedProduct.definition.id, NO_ADS, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                RemoveAds();
                // TODO: The non-consumable item has been successfully purchased, grant this item to the player.
            }
            // Or ... an unknown product has been purchased by this user. Fill in additional products here....
            else 
            {
                Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
            }

            // Return a flag indicating whether this product has completely been received, or if the application needs 
            // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
            // saving purchased products to the cloud, and when that save is delayed. 
            return PurchaseProcessingResult.Complete;
        }


        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
            // this reason with the user to guide their troubleshooting actions.
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        }
    }
}