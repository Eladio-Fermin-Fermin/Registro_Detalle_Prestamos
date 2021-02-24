using Microsoft.EntityFrameworkCore;
using Registro_Detalle_Prestamos.DAL;
using Registro_Detalle_Prestamos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Registro_Detalle_Prestamos.BLL
{
    public class PrestamosBLL
    {
        private Contexto Contexto { get; set; }

        public PrestamosBLL(Contexto contexto)
        {
            this.Contexto = contexto;

        }

        public async Task<bool> Guardar(Prestamos prestamo)
        {
            if (!await Existe(prestamo.PrestamoId))
            {

                return await Insertar(prestamo);
            }
            else
            {
                return await Modificar(prestamo);
            }
        }

        public async Task<bool> Existe(int id)
        {
            bool ok = false;

            try
            {
                ok = await Contexto.Prestamos.AnyAsync(p => p.PrestamoId == id);
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Insertar(Prestamos prestamo)
        {
            bool ok = false;

            try
            {
                prestamo.Balance = prestamo.Monto;
                await Contexto.Prestamos.AddAsync(prestamo);
                ok = await Contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Modificar(Prestamos prestamo)
        {
            bool ok = false;

            try
            {
                var aux = Contexto
                   .Set<Prestamos>()
                   .Local.FirstOrDefault(p => p.PrestamoId == prestamo.PrestamoId);

                if (aux != null)
                {
                    Contexto.Entry(aux).State = EntityState.Detached;
                }

                prestamo.Balance = prestamo.Monto;
                Contexto.Entry(prestamo).State = EntityState.Modified;

                ok = await Contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<Prestamos> Buscar(int id)
        {
            Prestamos prestamos;

            try
            {
                prestamos = await Contexto.Prestamos
                    .Where(p => p.PrestamoId == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return prestamos;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool ok = false;

            try
            {
                var registro = await Contexto.Prestamos.FindAsync(id);
                if (registro != null)
                {
                    Contexto.Prestamos.Remove(registro);
                    ok = await Contexto.SaveChangesAsync() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<List<Prestamos>> GetPrestamos()
        {
            List<Prestamos> lista = new List<Prestamos>();

            try
            {
                lista = await Contexto.Prestamos.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }

        public async Task<List<Prestamos>> GetPrestamos(Expression<Func<Prestamos, bool>> criterio)
        {
            List<Prestamos> lista = new List<Prestamos>();

            try
            {
                lista = await Contexto.Prestamos.Where(criterio).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }

       
        private List<MorasDetalles> BuscarMoras()
        {
            var lista = Contexto.Moras.Include(d => d.MorasDetalles).ToList();
            List<MorasDetalles> detalles = new List<MorasDetalles>();
            foreach (var item in lista)
            {
                foreach (var item2 in item.MorasDetalles)
                {
                    detalles.Add(item2);
                }
            }

            return detalles;
        }
    }
}
