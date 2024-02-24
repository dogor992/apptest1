using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Quiz2Cenfotec.DataContracts;
using System.Data;

namespace Quiz2Cenfotec.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public ApplicationDbContext()
        {

        }

        public virtual DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cliente>().HasData(new Cliente()
            {
             Id = 1,
             Nombre = "Jose",
             Apellidos="Vargas",
             Edad = 31,
             Cedula = "115180942215",
             Preferencias="Preferencias uno uno uno"
            });

            modelBuilder.Entity<Cliente>().HasData(new Cliente()
            {
                Id = 2,
                Nombre = "Jos",
                Apellidos = "Prado",
                Edad = 26,
                Cedula = "1168409454",
                Preferencias = "Preferencias dos dos dos"
            });
        }

        public _dg_gen_resultado InsertarCliente(Cliente cliente)
        {
            _dg_gen_resultado elResultado = new _dg_gen_resultado();
            var idParameter = new SqlParameter("@Id", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var nombreParameter = new SqlParameter("@Nombre", cliente.Nombre);
            var apellidosParameter = new SqlParameter("@Apellidos", cliente.Apellidos);
            var cedulaParameter = new SqlParameter("@Cedula", cliente.Cedula);
            var edadParameter = new SqlParameter("@Edad", cliente.Edad);
            var preferenciasParameter = new SqlParameter("@Preferencias", cliente.Preferencias);
            var codErrorParameter = new SqlParameter("@CodError", SqlDbType.NVarChar, 5) { Direction = ParameterDirection.Output };
            var mensajeParameter = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };

            Database.ExecuteSqlRaw("paInsertarClientes @Id OUTPUT, @Nombre, @Apellidos, @Cedula, @Edad, @Preferencias, @CodError OUTPUT, @Mensaje OUTPUT",
                idParameter, nombreParameter, apellidosParameter, cedulaParameter, edadParameter, preferenciasParameter, codErrorParameter, mensajeParameter);

            cliente.Id = (int)idParameter.Value;
            elResultado.CodError = (string)codErrorParameter.Value;
         
             //elResultado.Mensaje = (string)mensajeParameter.Value;
          
         

            return elResultado;
        }
        public _dg_gen_resultado ActualizarCliente(Cliente cliente)
        {
            _dg_gen_resultado elResultado = new _dg_gen_resultado();
            var idParameter = new SqlParameter("@Id", cliente.Id);
            var nombreParameter = new SqlParameter("@Nombre", cliente.Nombre);
            var apellidosParameter = new SqlParameter("@Apellidos", cliente.Apellidos);
            var cedulaParameter = new SqlParameter("@Cedula", cliente.Cedula);
            var edadParameter = new SqlParameter("@Edad", cliente.Edad);
            var preferenciasParameter = new SqlParameter("@Preferencias", cliente.Preferencias);
            var codErrorParameter = new SqlParameter("@CodError", SqlDbType.NVarChar, 5) { Direction = ParameterDirection.Output };
            var mensajeParameter = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };

            Database.ExecuteSqlRaw("paActualizarClientes @Id, @Nombre, @Apellidos, @Cedula, @Edad, @Preferencias, @CodError OUTPUT, @Mensaje OUTPUT",
                idParameter, nombreParameter, apellidosParameter, cedulaParameter, edadParameter, preferenciasParameter, codErrorParameter, mensajeParameter);

            elResultado.CodError = (string)codErrorParameter.Value;
           // elResultado.Mensaje = (string)mensajeParameter.Value;

            return elResultado;
        }

        public _dg_gen_resultado EliminarCliente(int id)
        {
            _dg_gen_resultado elResultado = new _dg_gen_resultado();
            var idParameter = new SqlParameter("@Id", id);
            var codErrorParameter = new SqlParameter("@CodError", SqlDbType.NVarChar, 5) { Direction = ParameterDirection.Output };
            var mensajeParameter = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };

            Database.ExecuteSqlRaw("paEliminarClientes @Id, @CodError OUTPUT, @Mensaje OUTPUT",
                idParameter, codErrorParameter, mensajeParameter);

            elResultado.CodError = (string)codErrorParameter.Value;
         

            return elResultado;
        }


    }
}
