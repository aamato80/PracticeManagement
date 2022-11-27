using FluentAssertions;
using FluentValidation.TestHelper;
using PracticeManagement.Api.DTOs;
using PracticeManagement.Api.Validators;
using PracticeManagement.Dal.Enums;
using PracticeManagement.Test.Mocks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PracticeManagement.Test.UnitTests.Validators
{
    public class TestPracticeDTOValidation
    {
        private PracticeDTOValidator _validator;
        public TestPracticeDTOValidation()
        {
            _validator = new PracticeDTOValidator();
        }


        [Fact]
        public void PracticeDTO_FirstName_Is_Null_ShouldFails()
        {
            var dto = PracticeDTOMock.Create();
            dto.FirstName = null;
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(person => person.FirstName).WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void PracticeDTO_FirstName_Is_GreaterThan_MaxValue_ShouldFails()
        {
            var dto = PracticeDTOMock.Create();
            dto.FirstName = Utils.CreateRandomString(101);
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(person => person.FirstName).WithErrorCode("MaximumLengthValidator");
        }

        [Fact]
        public void PracticeDTO_LastName_Is_Null_ShouldFails()
        {
            var dto = PracticeDTOMock.Create();
            dto.LastName = null;
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(person => person.LastName).WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void PracticeDTO_LastName_Is_GreaterThan_MaxValue_ShouldFails()
        {
            var dto = PracticeDTOMock.Create();
            dto.LastName = Utils.CreateRandomString(201);
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(person => person.LastName).WithErrorCode("MaximumLengthValidator");
        }

        [Fact]
        public void PracticeDTO_FiscalCode_Is_Null_ShouldFails()
        {
            var dto = PracticeDTOMock.Create();
            dto.FiscalCode = null;
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(person => person.FiscalCode).WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void PracticeDTO_FiscalCode_Is_GreaterThan_MaxValue_ShouldFails()
        {
            var dto = PracticeDTOMock.Create();
            dto.FiscalCode = Utils.CreateRandomString(17);
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(person => person.FiscalCode).WithErrorCode("MaximumLengthValidator");
        }

        [Fact]
        public void PracticeDTO_BirthDate_Is_Null_ShouldFails()
        {
            var dto = PracticeDTOMock.Create();
            dto.BirthDate = null;
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(person => person.BirthDate).WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void PracticeDTO_BirthDate_Is_MinimumDate_ShouldFails()
        {
            var dto = PracticeDTOMock.Create();
            dto.BirthDate = new DateTime(1,1,1);
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(person => person.BirthDate).WithErrorCode("GreaterThanValidator");
        }


        [Fact]
        public void PracticeDTO_Attachment_Is_Null_ShouldFails()
        {
            var dto = PracticeDTOMock.Create();
            dto.Attachment = null;
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(person => person.Attachment).WithErrorCode("NotEmptyValidator");
        }
    }
}
