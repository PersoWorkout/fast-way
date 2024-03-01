using Application.Interfaces;
using AutoMapper.Execution;
using Domain.Models;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IntegrationTests.Repositories
{
    public class UserRepsitoryTest : BaseIntegrationTest
    {
        private readonly IUserRepository _userRepository;
        public UserRepsitoryTest(IntegrationWebApplicationFactory factory) : 
            base(factory)
        {
            _userRepository = _scope.ServiceProvider
                .GetRequiredService<IUserRepository>();
        }

        private const string DEFAULT_FIRSTNAME = "John";
        private const string DEFAULT_LASTNAME = "Doe";
        private const string DEFAULT_EMAIL = "john.doe@example.com";
        private const string DEFAULT_PASSWORD = "Password123!";

        [Fact]
        public async Task GetAll_ShouldReturnUserList()
        {
            //Arrange
            //Act
            var users = await _userRepository.GetAll();

            //Assert
            Assert.IsType<List<User>>(users);
        }

        [Fact]
        public async Task Create_ShouldCreateUser()
        {
            //Arrange
            var user = CreateUser(firstname: "John");

            //Act
            var createdUser = await _userRepository.Create(user);

            //Assert
            var existInDb = 
                await _dbContext
                .Users.AnyAsync(
                    x => x.Id == createdUser.Id);

            Assert.True(existInDb);

            await ClearUsersTable();
        }

        [Fact]
        public async Task Create_ShouldThrowAnException_WhenEmailIsAlreadyUsed()
        {
            //Arrange
            var user = CreateUser(
                firstname: "Jane",
                email: "jane.doe@example.com");

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            //Act
            var newUser = CreateUser(firstname: "Doe");

            async Task action() => await _userRepository.Create(newUser);

            //Assert
            try
            {
                await Assert.ThrowsAnyAsync<DbUpdateException>(action);
            }
            catch { }
            finally
            {
                await ClearUsersTable();
            }   
        }

        [Fact]
        public async Task GetById_ShouldReturnUser_WhenIdExist()
        {
            //Arrange
            var user = CreateUser(
                firstname: "Joe", 
                email: "joe.doe@example.com");

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            //Act
            var userById = await _userRepository.GetById(user.Id);
            
            //Arrange
            Assert.NotNull(userById);
            Assert.Equal(user.Email, userById.Email);

            await ClearUsersTable();
        }

        [Fact]
        public async Task GetById_ShouldReturNull_WhenIdNotExist()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var userById = await _userRepository.GetById(id);

            //Arrange
            Assert.Null(userById);
        }


        [Fact]
        public async Task EmailAlreadyUsed_ShouldReturnTrue_WhenEmailExist()
        {
            //Arrange
            const string Email = "michael.doe@example.com";
            var emailValue = EmailValueObject
                .Create(Email).Data!;

            var user = CreateUser(
                firstname: "michael",
                email: Email);

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            //Act
            var result = await _userRepository
                .EmailAlreadyUsed(emailValue);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task EmailAlreadyUsed_ShouldReturnFalse_WhenEmailNotExist()
        {
            //Arrange
            const string Email = "test@test.com";
            var emailValue = EmailValueObject
                .Create(Email).Data!;

            //Act
            var result = await _userRepository
                .EmailAlreadyUsed(emailValue);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetByEmail_ShouldReturnUser_WhenEmailExist()
        {
            //Arrange
            const string Email = "johnson.doe@example.com";
            var emailValue = EmailValueObject
                .Create(Email).Data!;

            var existingUser = CreateUser(
                firstname: "Johnson",
                email: Email);

            await _dbContext.Users.AddAsync(existingUser);
            await _dbContext.SaveChangesAsync();

            //Act
            var user = await _userRepository
                .GetByEmail(emailValue);

            //Assert
            Assert.NotNull(user);
            Assert.Equal(Email, user.Email.Value);
        }

        [Fact]
        public async Task GetByEmail_ShouldReturnNull_WhenEmailNotExist()
        {
            //Arrange
            const string Email = "johnson.doe@example.com";
            var emailValue = EmailValueObject
                .Create(Email).Data!;

            //Act
            var user = await _userRepository
                .GetByEmail(emailValue);

            //Assert
            Assert.Null(user);
        }

        [Fact]
        public async Task Update_ShouldBeUpdateUser()
        {
            //Arrange
            const string Email = "andrea.doe@example.com";

            var user = CreateUser(
                firstname: "Andrea",
                email: Email);

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            const string NewFirstName = "Isabelle";

            //Act
            user.Update(firstname: NewFirstName);
            user = await _userRepository.Update(user);

            //Assert
            var userInDb = await _dbContext.Users.FindAsync(user.Id);
            Assert.Equal(NewFirstName, userInDb!.Firstname);
        }

        [Fact]
        public async Task Delete_ShouldDeleteUser()
        {
            //Arrange
            const string Email = "carolina.doe@example.com";

            var user = CreateUser(
                firstname: "Carolina",
                email: Email);

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            //Act
            var result = await _userRepository.Delete(user);

            //Assert
            var userInDb = await _dbContext.Users.FindAsync(user.Id);
            Assert.Equal(1, result);
            Assert.Null(userInDb);
        }

        private static User CreateUser(
            string firstname = DEFAULT_FIRSTNAME,
            string lastname = DEFAULT_LASTNAME,
            string email = DEFAULT_EMAIL,
            string password = DEFAULT_PASSWORD)
        {
            return new User
            {
                Firstname = firstname,
                Lastname = lastname,
                Email = EmailValueObject
                    .Create(email).Data!,
                Password = PasswordValueObject
                    .Create(password).Data!
            };
        }

        private async Task ClearUsersTable()
        {
            var users = await _dbContext.Users.ToListAsync();
            _dbContext.Users.RemoveRange(users);
            await _dbContext.SaveChangesAsync();

            foreach(User user in users) {
                _dbContext.Entry(user).Reload();
            }
        }
    }
}
