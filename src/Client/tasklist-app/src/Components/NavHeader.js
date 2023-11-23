import React from "react";
import { Navbar, Nav } from "react-bootstrap";
export const NavHeader = ({
  isLoggedIn,
  user,
  onLoginClicked,
  onSignupClicked,
  onLogoutClicked,
  onAddTaskClicked,
  onRefreshClicked,
}) => {
  return (
    <Navbar>
      <Navbar.Brand href="#home">Todo Tasks</Navbar.Brand>
      <Nav className="me-auto">
        {isLoggedIn && (
          <>
            <Nav.Link href="#home">Welcome {user.userName}</Nav.Link>
            <Nav.Link href="#features" onClick={onLogoutClicked}>
              Logout
            </Nav.Link>
            <Nav.Link href="#features" onClick={onAddTaskClicked}>
              Add Task
            </Nav.Link>
            <Nav.Link href="#features" onClick={onRefreshClicked}>
              Refresh
            </Nav.Link>
          </>
        )}
        {!isLoggedIn && (
          <>
            <Nav.Link href="#home" onClick={onLoginClicked}>
              Login
            </Nav.Link>
            <Nav.Link href="#features" onClick={onSignupClicked}>
              Sign up
            </Nav.Link>
          </>
        )}
      </Nav>
    </Navbar>
  );
};
export default NavHeader;
