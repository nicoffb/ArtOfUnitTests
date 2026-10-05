// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tdd_CreatePatientBasedOnHisBirthdate4.cs" company="">
//   
// </copyright>
// <summary>
//   The unit test 2.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace TheArtOfUnitTesting.Services.UnitTests.Exercise4
{
    /// <summary>
    /// The tdd_ create patient based on his birthdate 4.
    /// </summary>
    [TestFixture]
    public class Tdd_CreatePatientBasedOnHisBirthdate4
    {
        /// <summary>
        /// The patient repository.
        /// </summary>
        private IPatientRepository patientRepository;

        /// <summary>
        /// The patient repository mock.
        /// </summary>
        private Mock<IPatientRepository> patientRepositoryMock;

        [SetUp]
        public void Initialize()
        {
            this.patientRepositoryMock = new Mock<IPatientRepository>();
            this.patientRepository = this.patientRepositoryMock.Object;
        }


        #region Public Methods and Operators

        /// <summary>
        /// The ac c 1_ create_patient_with_wrong_name.
        /// </summary>
        [Test]
        public void ACC1_Create_patient_with_wrong_name()
        {
            // AAA
            var error = Assert.Throws<DomainException>(() => this.CreatePatient(null, DateTime.Now.AddDays(-3)));
            Assert.IsTrue(error.DomainErrorCode == 1);
        }

        /// <summary>
        /// The ac c 2_ create_patient_with_wrong_birthdate.
        /// </summary>
        [Test]
        public void ACC2_Create_patient_with_wrong_birthdate()
        {
            // AAA
            var error = Assert.Throws<DomainException>(() => this.CreatePatient("Benjami", DateTime.Now.AddDays(3)));
            Assert.IsTrue(error.DomainErrorCode == 2);
        }

        /// <summary>
        /// The ac c 3_ create_a_father_patient.
        /// </summary>
        [Test]
        public void ACC3_Create_a_father_patient()
        {
            // Arrange
            // Act
            var patient = this.CreatePatient("Benjami", DateTime.Now.AddYears(-31).Date);

            // Assert
            Assert.IsNotNull(patient);
            Assert.IsTrue(patient.Name == "Benjami");
            Assert.IsTrue(patient.BirthDate == DateTime.Now.AddYears(-31).Date);
            Assert.IsTrue(patient.Type == (int)PatientType.Father);
        }

        /// <summary>
        /// The ac c 4_ create_a_father_patient.
        /// </summary>
        [Test]
        public void ACC4_Create_a_no_father_patient()
        {
            // Arrange
            // Act
            var patient = this.CreatePatient("Benjami", DateTime.Now.AddYears(-1).Date);

            // Assert
            Assert.IsNotNull(patient);
            Assert.IsTrue(patient.Name == "Benjami");
            Assert.IsTrue(patient.BirthDate == DateTime.Now.AddYears(-1).Date);
            Assert.IsTrue(patient.Type == (int)PatientType.NoFather);
        }

        /// <summary>
        /// The technical specs_save_patient_name_to_disk.
        /// </summary>
        [Test]
        public void TechnicalSpecs_save_patient_name_to_disk()
        {
            // Arrange
            patientRepositoryMock
                .Setup(rep => rep.Save(It.IsAny<Patient>()))
                .Verifiable("Patient has not been saved.");

            // Act
            var patient = this.CreatePatient("Benjami", DateTime.Now.AddYears(-1).Date);

            // Assert
            Assert.IsNotNull(patient);
            Assert.IsTrue(patient.Name == "Benjami");
            Assert.IsTrue(patient.BirthDate == DateTime.Now.AddYears(-1).Date);
            Assert.IsTrue(patient.Type == (int)PatientType.NoFather);
            patientRepositoryMock.VerifyAll();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The create patient.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="birthdate">
        /// The birthdate.
        /// </param>
        /// <returns>
        /// The <see cref="Patient"/>.
        /// </returns>
        /// <exception cref="DomainException">
        /// </exception>
        private Patient CreatePatient(string name, DateTime birthdate)
        {
            if (string.IsNullOrEmpty(name))
            {
                // This is a domain logic error, not technical error.
                throw new DomainException("Domain Error: Patient cannot be created with wrong name.", 1);
            }

            if (birthdate.Date >= DateTime.Today)
            {
                // This is a domain logic error, not technical error.
                throw new DomainException("Domain Error: Patient birthdate has to be less than today", 2);
            }

            var patient = new Patient() { PatientId = 1, Name = name, BirthDate = birthdate };

            if (birthdate.Year <= DateTime.Now.AddYears(-30).Year)
            {
                patient.Type = (int)PatientType.Father;
            }
            else
            {
                patient.Type = (int)PatientType.NoFather;
            }

            this.patientRepository.Save(patient);

            return patient;
        }


        #endregion
    }
}