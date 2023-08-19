using My_Ap.BL;

namespace My_Ap.BL
{
    public class Customers : User
    {
        String role;
        public Customers(
         String id,
         String name,
         String userId,
         String address,
         DateTime createDate,
         String ibn,
         DateTime dob,
         String email,
         String password,
         String role
            ):base(
          id,
          name,
          userId,
          address,
          createDate,
          ibn,
          dob,
          email,
          password
             ) { }
        public Customers():base() { }
        public string Role { get => role; set => role = value; }

    }
}
