using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Presentation.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServiceNotaFiscal : InterfaceNotaFiscal
    {
        private readonly AppDbContext _context;

        public ServiceNotaFiscal(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NotaFiscalDTO>> GetAllAsync()
        {
            var lista = await _context.NotasFiscais.ToListAsync();
            return lista.Select(n => new NotaFiscalDTO
            {
                Id = n.Id,
                Numero = n.Numero,
                DataEmissao = n.DataEmissao,
                ValorTotal = n.ValorTotal
            });
        }

        public async Task<NotaFiscalDTO> GetByIdAsync(long id)
        {
            var entidade = await _context.NotasFiscais.FindAsync(id);
            if (entidade == null)
                return null;

            return new NotaFiscalDTO
            {
                Id = entidade.Id,
                Numero = entidade.Numero,
                DataEmissao = entidade.DataEmissao,
                ValorTotal = entidade.ValorTotal
            };
        }

        public async Task<NotaFiscalDTO> CreateAsync(NotaFiscalDTO dto)
        {
            try
            {
                var entidade = new NotaFiscalEntity
                {
                    Id = dto.Id,
                    Numero = dto.Numero,
                    DataEmissao = dto.DataEmissao,
                    ValorTotal = dto.ValorTotal
                };

                _context.NotasFiscais.Add(entidade);
                await _context.SaveChangesAsync();

                dto.Id = entidade.Id;
                return dto;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao criar NotaFiscal: " + ex.Message);
            }
        }

        public async Task<NotaFiscalDTO> UpdateAsync(long id, NotaFiscalDTO dto)
        {
            try
            {
                var entidade = await _context.NotasFiscais.FindAsync(id);
                if (entidade == null)
                    return null;

                entidade.Numero = dto.Numero;
                entidade.DataEmissao = dto.DataEmissao;
                entidade.ValorTotal = dto.ValorTotal;

                _context.NotasFiscais.Update(entidade);
                await _context.SaveChangesAsync();

                return dto;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao atualizar NotaFiscal: " + ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            try
            {
                var entidade = await _context.NotasFiscais.FindAsync(id);
                if (entidade == null)
                    return false;

                _context.NotasFiscais.Remove(entidade);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao excluir NotaFiscal: " + ex.Message);
            }
        }

        public async Task<IEnumerable<NotaFiscalDTO>> GetByNumeroAsync(string numero)
        {
            var lista = await _context.NotasFiscais
                .Where(n => n.Numero.ToLower().Contains(numero.ToLower()))
                .ToListAsync();

            return lista.Select(n => new NotaFiscalDTO
            {
                Id = n.Id,
                Numero = n.Numero,
                DataEmissao = n.DataEmissao,
                ValorTotal = n.ValorTotal
            });
        }
    }
}
