using FluentAssertions;
using FluentValidation.TestHelper;
using DossierManagement.Api.DTOs;
using DossierManagement.Api.Validators;
using DossierManagement.Dal.Enums;
using DossierManagement.Test.Mocks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DossierManagement.Test.UnitTests.Validators
{
    public class TestDossierDtoValidation
    {
        private DossierDtoValidator _validator;
        public TestDossierDtoValidation()
        {
            _validator = new DossierDtoValidator();
        }


        [Fact]
        public void DossierDto_FirstName_Is_Null_ShouldFails()
        {
            var dto = DossierDtoMock.Create();
            dto.FirstName = null;
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(person => person.FirstName).WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void DossierDto_FirstName_Is_GreaterThan_MaxValue_ShouldFails()
        {
            var dto = DossierDtoMock.Create();
            dto.FirstName = Utils.CreateRandomString(101);
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(person => person.FirstName).WithErrorCode("MaximumLengthValidator");
        }

        [Fact]
        public void DossierDto_LastName_Is_Null_ShouldFails()
        {
            var dto = DossierDtoMock.Create();
            dto.LastName = null;
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(person => person.LastName).WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void DossierDto_LastName_Is_GreaterThan_MaxValue_ShouldFails()
        {
            var dto = DossierDtoMock.Create();
            dto.LastName = Utils.CreateRandomString(201);
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(person => person.LastName).WithErrorCode("MaximumLengthValidator");
        }

        [Fact]
        public void DossierDto_FiscalCode_Is_Null_ShouldFails()
        {
            var dto = DossierDtoMock.Create();
            dto.FiscalCode = null;
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(person => person.FiscalCode).WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void DossierDto_FiscalCode_Is_GreaterThan_MaxValue_ShouldFails()
        {
            var dto = DossierDtoMock.Create();
            dto.FiscalCode = Utils.CreateRandomString(17);
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(person => person.FiscalCode).WithErrorCode("MaximumLengthValidator");
        }

        [Fact]
        public void DossierDto_BirthDate_Is_Null_ShouldFails()
        {
            var dto = DossierDtoMock.Create();
            dto.BirthDate = null;
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(person => person.BirthDate).WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void DossierDto_BirthDate_Is_MinimumDate_ShouldFails()
        {
            var dto = DossierDtoMock.Create();
            dto.BirthDate = new DateTime(1,1,1);
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(person => person.BirthDate).WithErrorCode("GreaterThanValidator");
        }


        [Fact]
        public void DossierDto_Attachment_Is_Null_ShouldFails()
        {
            var dto = DossierDtoMock.Create();
            dto.Attachment = null;
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(person => person.Attachment).WithErrorCode("NotEmptyValidator");
        }
    }
}
