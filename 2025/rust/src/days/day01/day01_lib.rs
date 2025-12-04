
// Define a Rotation rust enum to represent a rotation operation with two variants for each direction (either Left or Right) 
// every instance of the enum has a number of clicks which is an integer between 0 and 99
pub enum Rotation {
  Left(i32),
  Right(i32),
}
