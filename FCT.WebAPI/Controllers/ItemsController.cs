using FCT.WebAPI.Data;
using FCT.WebAPI.Helpers;
using FCT.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FCT.WebAPI.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class ItemsController : Controller {
        private IItemRepo ItemRepo { get; set; }
        public ItemsController (IItemRepo _ItemRepo) {
            ItemRepo = _ItemRepo;
        }

        // Store an item to the repository.
        // Parameter itemType is used to differentiate JSON or XML.
        // 1 = itemContent is a JSON string.
        // 2 = itemContent is an XML string.
        [HttpPost ("register")]
        public IActionResult Register (RegisterModel registerModel) {
            Item item = new Item ();
            item.ItemName = registerModel.ItemName;
            item.ItemType = registerModel.ItemType;
            if (registerModel.ItemType == 1) {
                if (registerModel.ItemContent.IsValidJson ()) {
                    item.ItemContent = registerModel.ItemContent;
                } else {
                    return BadRequest ("Item does not contain Json");
                }
            } else if (registerModel.ItemType == 2) {
                if (registerModel.ItemContent.IsValidXml ()) {
                    item.ItemContent = registerModel.ItemContent;
                } else {
                    return BadRequest ("Item does not contain XML");
                }
            } else {
                return BadRequest ("Item Type and Item Content are not match Json / XML.");
            }

            Item itemOrig = ItemRepo.RetrieveItem (registerModel.ItemName);
            if (itemOrig == null) {
                ItemRepo.RegisterItem (item);
            } else {
                return BadRequest ("Item already exists");
            }
            return StatusCode (201);
        }

        // // Retrieve an item from the repository.
        // public string Retrieve (string itemName) { }
        [HttpGet ("retrieve")]
        public IActionResult Retrieve (string itemName) {
            Item itemOrig = ItemRepo.RetrieveItem (itemName);
            if (itemOrig != null) {
                var data = new {
                ItemName = itemOrig.ItemName, ItemContent = itemOrig.ItemContent, ItemType = itemOrig.ItemType
                };
                return StatusCode (200, data);
            } else {
                return StatusCode (404);
            }
        }

        // // Retrieve the type of the item (JSON or XML).
        // public int GetType (string itemName) { }
        [HttpGet ("gettype")]
        public IActionResult GetType (string itemName) {
            Item itemOrig = ItemRepo.RetrieveItem (itemName);
            if (itemOrig != null) {
                return StatusCode (200, itemOrig.ItemType);
            } else {
                return StatusCode (404);
            }
        }

        // // Remove an item from the repository.
        // public void Deregister (string itemName) { }
        [HttpDelete ("deregister")]
        public IActionResult Deregister (string itemName) {
            Item itemOrig = ItemRepo.RetrieveItem (itemName);
            if (itemOrig == null) {
                return StatusCode (204, "Item does not exists.");
            } else {
                ItemRepo.Deregister (itemName);
            }
            return StatusCode (200, "Item has been unregistered!");

        }

    }
}