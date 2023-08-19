using My_Ap.BL;
namespace My_Ap.DL

{
    public class AuthenticationDL
    {
     
        User validUser(String email,String password)
        {
            User user = new User();
            return user;

        }

        private User getDataFromDB(String email, String password)
        {
            User user = new User();
            return user;
        }


         User getUserData(String id)
         {
            User user = new User();
            return user;
         }
    }
}
