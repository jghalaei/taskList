import { useState } from "react";
import React from "react";
import { Form, FormGroup, Button, Row } from "react-bootstrap";

const LoginForm = ({ onLogin }) => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const backendUrl = process.env.REACT_APP_BACKEND_URL;
  const onClick = async () => {
    const loginData = {
      UserName: username,
      givenPassword: password,
    };
    try {
      const res = await fetch(`${backendUrl}/user/login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(loginData),
      });
      if (res.status === 200) {
        const user = await res.json();
        console.log("user from server", user);
        onLogin(user);
      } else {
        const error = await res.text();
        alert(error);
      }
    } catch (error) {
      alert(error);
    }
  };
  return (
    <Form className="w-50 p-3 bg-light rounded mx-auto  shadow border center ">
      <FormGroup>
        <Form.Label>Username</Form.Label>
        <Form.Control
          onChange={(e) => setUsername(e.target.value)}
          value={username}
          type="text"
        />
      </FormGroup>
      <FormGroup>
        <Form.Label>Password</Form.Label>
        <Form.Control
          onChange={(e) => setPassword(e.target.value)}
          value={password}
          type="password"
        />
      </FormGroup>
      <Row className="mt-3 justify-content-right">
        <Button
          variant="primary"
          type="submit"
          className="me-3 mb-3 mx-auto w-25"
          onClick={onClick}
        >
          Login
        </Button>
      </Row>
    </Form>
  );
};

export default LoginForm;
