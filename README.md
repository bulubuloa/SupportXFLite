
# SupportXFLite

A simple MVVM Framework for Xamarin Forms. It helps you to quicksetup a new Xamarin.Forms project and save a lot of your time with a few steps.. I used this library in my personal project and my company also every day :)

#### Available on NuGet
![Build status](https://ci.appveyor.com/api/projects/status/7g3sppml9ewumr9i/branch/master?svg=true) [![NuGet Badge](https://buildstats.info/nuget/SupportXFLite)](https://www.nuget.org/packages/SupportXFLite/)

## GETTING STARTED

### 1.  Create class manager you project, it based on **SupportProjectXF** class
```
public class InitializeProject : SupportProjectXF<BaseLocator, BaseNavigationService>
{
	private async Task<SupportNavigationPageWidget> InitilizeNavigationPageWithPage(Xamarin.Forms.Page page)
        {
            var navigationPageWidget = new SupportNavigationPageWidget()
            {
                BarTextColor = Color.White,
                BarBackgroundColor = Color.FromHex("#005e93")
            };
            if (page != null)
                await navigationPageWidget.PushAsync(page);
            return navigationPageWidget;
        }

	/*  
	*  Custom your app flow view
	*/
        public async override Task SetupNavigationMap(Page page, Type viewModelType, object parameter, bool animate)
        {
            if (page is AES_LoginView)
            {
                if (CurrentApplication.MainPage is SupportNavigationPageWidget)
                {
                    var currentNavigation = CurrentApplication.MainPage as SupportNavigationPageWidget;
                    await currentNavigation.PopToRootAsync(true);
                }
                else
                {
                    CurrentApplication.MainPage = await InitilizeNavigationPageWithPage(page);
                }
            }
            else if (CurrentApplication.MainPage is SupportNavigationPageWidget)
            {
                var currentNavigationX = (SupportNavigationPageWidget)CurrentApplication.MainPage;
                var currentPage = currentNavigationX.CurrentPage;

                if (page.GetType() != currentNavigationX.CurrentPage.GetType())
                        await currentNavigationX.PushAsync(page, true);
            }
            else
            {
                CurrentApplication.MainPage = await InitilizeNavigationPageWithPage(page);
            }
        }
		
	/*  
	*  Register your controller, based on dependency injection
	*/
        protected override void RegisterController(BaseLocator locator)
        {
            locator.Register<IAESAPIService, AESAPIService>();
        }
        
	/*  
	*  Register your viewmodel or any class, based on dependency injection
	*/
        protected override void RegisterViewModel(BaseLocator locator)
        {
            locator.Register<AES_LoginViewModel>();
            locator.Register<AES_MainViewModel>();
        }
		
	/*  
	*  map your view page with viewmodel
	*/
        protected override void MappingViewAndViewModel(BaseNavigationService navigationManager)
        {
            navigationManager.Map<AES_LoginViewModel, AES_LoginView>();
            navigationManager.Map<AES_MainViewModel, AES_MainView>();
        }
}
```


### 2. Create new object of Manager
- Open App.xaml.cs and add it to content
```
	 public  static  InitializeProject  Manager;  
	 static  App()  
	 {  
		Manager  =  new  InitializeProject();  
	 }
```
- In the contructor of App
```
	 public  App()  
	{  
		InitializeComponent();  
		
		//Open first screen
		var  navigationService  =  Manager.NavigationManager;  
		navigationService.NavigateToAsync<AES_LoginViewModel>(true);  
	}
```

### 3. Setup for each platforms
Setup for iOS project (add to AppDelegate before LoadApplication)

    SupportXFLiteSetup.Initialize(this);

Setup for Android project (add to MainActivity before LoadApplication)

    SupportXFLiteSetup.Initialize(this, savedInstanceState);

  
### DEMO APP
https://github.com/bulubuloa/DefaultAPP_Lite
