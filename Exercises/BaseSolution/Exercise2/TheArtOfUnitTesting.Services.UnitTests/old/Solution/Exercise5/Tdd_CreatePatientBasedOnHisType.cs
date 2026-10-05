// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tdd_CreatePatientBasedOnHisType.cs" company="">
//   
// </copyright>
// <summary>
//   The unit test 2.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace TheArtOfUnitTesting.Services.UnitTests.Exercise5
{
    /// <summary>
    /// The tdd_ create patient based on his birthdate 5.
    /// </summary>
    [TestFixture]
    public class Tdd_CreatePatientBasedOnHisType
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
            var error = Assert.Throws<DomainException>(
                () => new PatientsService(patientRepository).CreatePatient(null, (int)PatientType.Father));
            Assert.IsTrue(error.DomainErrorCode == 1);
        }

        /// <summary>
        /// The ac c 2_ create_patient_with_wrong_type.
        /// </summary>
        [Test]
        public void ACC2_Create_patient_with_wrong_type()
        {
            // AAA
            var error = Assert.Throws<DomainException>(
                () => new PatientsService(patientRepository).CreatePatient("Benjami", 99));
            Assert.IsTrue(error.DomainErrorCode == 2);
        }

        /// <summary>
        /// The ac c 3_ create_a_father_patient.
        /// </summary>
        [Test]
        public void ACC3_Create_a_father_patient()
        {
            // Arrange
            var service = new PatientsService(patientRepository);

            // Act
            var patient = service.CreatePatient("Benjami", (int)PatientType.Father);

            // Assert
            Assert.IsNotNull(patient);
            Assert.IsTrue(patient.Name == "Benjami");
            Assert.IsTrue(patient.BirthDate == DateTime.Now.AddYears(-30).Date);
            Assert.IsTrue(patient.Type == (int)PatientType.Father);
        }

        /// <summary>
        /// The ac c 4_ create_a_father_patient.
        /// </summary>
        [Test]
        public void ACC4_Create_a_no_father_patient()
        {
            // Arrange
            var service = new PatientsService(patientRepository);

            // Act
            var patient = service.CreatePatient("Benjami", (int)PatientType.NoFather);

            // Assert
            Assert.IsNotNull(patient);
            Assert.IsTrue(patient.Name == "Benjami");
            Assert.IsTrue(patient.BirthDate == DateTime.Now.AddYears(-25).Date);
            Assert.IsTrue(patient.Type == (int)PatientType.NoFather);
        }

        /// <summary>
        /// The technical specs_save_patient_name_to_disk.
        /// </summary>
        [Test]
        public void TechnicalSpecs_save_patient_name_to_disk()
        {
            // Arrange
            var service = new PatientsService(patientRepository);
            patientRepositoryMock.Setup(rep => rep.Add(It.IsAny<Patient>())).Verifiable("Patient has not been added.");
            patientRepositoryMock.Setup(rep => rep.Save()).Verifiable("Patient has not been saved.");

            // Act
            var patient = service.CreatePatient("Benjami", (int)PatientType.Father);

            // Assert
            Assert.IsNotNull(patient);
            Assert.IsTrue(patient.Name == "Benjami");
            Assert.IsTrue(patient.BirthDate == DateTime.Now.AddYears(-30).Date);
            Assert.IsTrue(patient.Type == (int)PatientType.Father);
            patientRepositoryMock.VerifyAll();
        }

        #endregion

    }
}