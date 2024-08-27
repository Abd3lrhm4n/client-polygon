﻿using Application.Dtos;
using Application.Services.Client;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ClientsCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(CreateClientDto dto)
        {
            try
            {
                await _clientService.AddClientAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _clientService.GetAllClientsAsync();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(long id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null) return NotFound();

            return Ok(client);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(long id, UpdateClientDto dto)
        {
            try
            {
                await _clientService.UpdateClientAsync(id, dto);
                return Ok(new {message = "Client updated successfully", success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(long id)
        {
            try
            {
                await _clientService.DeleteClientAsync(id);
                return Ok(new { message = "Client deleted successfully", success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetClientsPaginAsync")]
        public async Task<IActionResult> GetClientsPaginAsync(GridStateDto gridState)
        {
            return Ok(await _clientService.GetClientsPaginAsync(gridState));
        }
    }
}
