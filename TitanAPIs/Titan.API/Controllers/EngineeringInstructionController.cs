using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Titan.API.Models;
using Titan.API.Models.EngineeringInstructions;

namespace Titan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EngineeringInstructionController : Controller
    {
        internal TitanContext Titan { get; private set; }

        public EngineeringInstructionController(TitanContext titan)
        {
            Titan = titan;
        }

        private EngineeringInstructionDTO GetEIWithChildren(EngineeringInstruction EI)
        {
            return new EngineeringInstructionDTO()
            {
                EIRef = EI.EIRef,
                IsTopLevel = EI.IsTopLevel,
                Children = Titan.EIRelationships
                    .Where(EIR => EIR.ParentId == EI.EIRef)
                    // select EIs for each of the child refs
                    .Join(
                        Titan.EngineeringInstructions,
                        EIR => EIR.ChildId,
                        EIChild => EIChild.EIRef,
                        (EIR, EIChild) => EIChild
                    )
                    .OrderBy(EIChild => EIChild.EIRef)
                    .ToList()
                    // get children recursively
                    .Select(EIChild => GetEIWithChildren(EIChild))
                    .ToList()
            };
        }

        private void EnsureTopLevelIsCorrect()
        {
            var EIsThatAreChildren = Titan.EIRelationships
                .Select(EIR => EIR.ChildId)
                .Distinct()
                .ToList();

            var EIsThatAreParents = Titan.EIRelationships
                .Select(EIR => EIR.ParentId)
                .Distinct()
                .ToList();

            var EIsThatAreTopLevel = EIsThatAreParents.Except(EIsThatAreChildren);

            // update children to show they aren't top level
            Titan.EngineeringInstructions
                .Where(EI => EIsThatAreChildren.Contains(EI.EIRef))
                .ToList()
                .ForEach(EI => EI.IsTopLevel = false);

            // update parents to show they are top level
            Titan.EngineeringInstructions
                .Where(EI => EIsThatAreTopLevel.Contains(EI.EIRef))
                .ToList()
                .ForEach(EI => EI.IsTopLevel = true);

            Titan.SaveChanges();
        }

        /// <response code="200">Succesfully returns a list of engineering instructions</response>
        [HttpGet]
        [Route("List")]
        public ActionResult<List<EngineeringInstruction>> ListEIs()
        {
            return Titan.EngineeringInstructions.ToList();
        }

        /// <response code="200">Succesfully returns an engineering instruction 
        /// and its children</response>
        [HttpGet]
        [Route("{EIRef}")]
        public ActionResult<EngineeringInstructionDTO> GetEI(string EIRef)
        {
            EnsureTopLevelIsCorrect();

            var EIParent = Titan.EngineeringInstructions
                .Where(EI => EI.EIRef == EIRef)
                .SingleOrDefault();

            if (EIParent == null)
            {
                return NotFound();
            }

            var EIWithChildren = GetEIWithChildren(EIParent);

            return EIWithChildren;
        }

        /// <response code="200">Succesfully updates engineering instruction children</response>
        [HttpPut]
        [Route("{EIRef}/Children")]
        public ActionResult EditEIChildren(string EIRef, List<string> Children)
        {
            var EIParent = Titan.EngineeringInstructions
                .Where(EI => EI.EIRef == EIRef)
                .SingleOrDefault();

            if (EIParent == null)
            {
                return NotFound();
            }

            var EIChildren = Titan.EIRelationships
                .Where(EIR => EIR.ParentId == EIParent.EIRef)
                .OrderBy(EIR => EIR.ChildId)
                .Select(EIR => EIR.ChildId)
                .ToList();

            // if there are no differences, no changes are needed
            if (EIChildren.Count() == Children.Count &&
                EIChildren.SequenceEqual(Children.OrderBy(EIRef => EIRef)))
            {
                return Ok();
            }

            // remove all children
            Titan.EIRelationships.RemoveRange(
                Titan.EIRelationships.Where(EIR => EIR.ParentId == EIParent.EIRef)
            );

            // add updated children
            Titan.EIRelationships.AddRange(
                Children.Select(EIChildRef => new EIRelationship()
                {
                    ChildId = EIChildRef,
                    ParentId = EIParent.EIRef
                })
            );

            EnsureTopLevelIsCorrect();
            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Succesfully added an engineering instruction</response>
        [HttpPost]
        [Route("{EIRef}/Create")]
        public ActionResult CreateEI(string EIRef)
        {
            var EIParent = Titan.EngineeringInstructions
                .Where(EI => EI.EIRef == EIRef)
                .SingleOrDefault();

            if (EIParent != null)
            {
                return Conflict();
            }

            Titan.EngineeringInstructions.Add(new EngineeringInstruction()
            {
                EIRef = EIRef,
                IsTopLevel = true
            });

            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Returns results for autocomplete</response>
        [HttpGet]
        [Route("{EIRef}/AutoComplete")]
        public ActionResult<List<string>> AutoCompleteEI(string EIRef)
        {
            return Titan.EngineeringInstructions
                .Where(EI => EI.EIRef.Contains(EIRef))
                .Select(EI => EI.EIRef)
                .OrderBy(EIRefGuess => EIRefGuess.Length)
                .ToList();
        }
    }
}
