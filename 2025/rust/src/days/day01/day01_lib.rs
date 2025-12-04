// Define a Rotation rust enum to represent a rotation operation with two variants for each direction (either Left or Right)
// every instance of the enum has a number of clicks which is an integer between 0 and 99
pub enum Rotation {
  Left(i32),
  Right(i32),
}

// Given a rotation definition and a starting position start_pos,
// determine if the rotation causes the arrow to point to zero at any point during its movement
pub fn causes_pointing_to_zero(rotation: &Rotation, start_pos: i32) -> bool {
  let mut arrow_position = start_pos;
  let sign = match rotation {
    Rotation::Left(_) => -1,
    Rotation::Right(_) => 1,
  };
  // iterate cyclically over the dial according to the rotation distance
  // Decrease or increase the arrow position according to the rotation direction
  // Stop if we reach zero returning true
  let distance = match rotation {
    Rotation::Left(dist) => *dist,
    Rotation::Right(dist) => *dist,
  };
  for _ in 0..distance {
    arrow_position = (arrow_position + sign + 100) % 100; // ensure cyclic behavior
    if arrow_position == 0 {
      return true;
    }
  }
  
  // If we complete the iterations it means we never pointed to zero
  false
}
