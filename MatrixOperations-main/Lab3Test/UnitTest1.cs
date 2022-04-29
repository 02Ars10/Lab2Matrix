using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MatrixOperations;

namespace Lab3Test
{
    [TestClass]
    public class GenericUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckingTypeOfMatrices()
        {
            GenericMatrix<char> genericMatrix = new GenericMatrix<char>(2, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Different_Multiply_Matrixes()
        {
            //Arrange
            GenericMatrix<int> a, b, c;
            a = new GenericMatrix<int>(4, 4);
            b = new GenericMatrix<int>(3, 3);

            //Act
            c = a * b;

            //Assert
            Assert.AreEqual(a[0, 0], b[0, 0], 1);
        }

        [TestMethod]
        public void Correct_Multiply_Matrixes()
        {
            //Arrange
            GenericMatrix<int> first, second, third, res;
            first = new GenericMatrix<int>(2, 2);
            second = new GenericMatrix<int>(2, 2);
            third = new GenericMatrix<int>(2, 2);
            res = new GenericMatrix<int>(2, 2);

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    first[i, j] = 1;
                }
            }
            second[0, 0] = 4;
            second[0, 1] = 5;

            third[0, 0] = 4;
            third[0, 1] = 5;
            third[1, 0] = 4;
            third[1, 1] = 5;

            //Act
            res = first * second;

            //Assert
            Assert.AreEqual(res[1, 1], third[1, 1], 0);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Different_Sum_Matrixes()
        {
            //Arrange
            GenericMatrix<int> a, b, c;
            a = new GenericMatrix<int>(4, 4);
            b = new GenericMatrix<int>(3, 3);

            //Act
            c = a + b;

            //Assert
            Assert.AreEqual(a[0, 0], b[0, 0], 1);
        }
            [TestMethod]
            public void Correct_Sum_Matrixes()
            {
                //Arrange
                GenericMatrix<int> a, b, c, d;
                a = new GenericMatrix<int>(2, 2);
                b = new GenericMatrix<int>(2, 2);
                c = new GenericMatrix<int>(2, 2);
                d = new GenericMatrix<int>(2, 2);

                a[1, 1] = 12;
                b[1, 1] = 8;
                c[1, 1] = 20;
                //Act
                d = a + b;

                //Assert
                Assert.AreEqual(d[1, 1], c[1, 1], 0);
            }
        [TestMethod]
        public void Generate_Matrix_Proverka_Lambda()
        {
            //Arrange
            GenericMatrix<int> a;

            //Act
            a = new GenericMatrix<int>(2, 2, (i, j) => 
                i * j);
            

            //Assert
            Assert.AreEqual(a[1, 1], 1, 0);
        }
    }
    }

