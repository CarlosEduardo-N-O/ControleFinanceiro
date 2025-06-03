// src/pages/LoginPage.jsx
import React from "react";
import Login from "../components/Login";

const LoginPage = ({ onLogin }) => {
    return (
        <div className="login-page">
            <Login onLogin={onLogin} />
        </div>
    );
};

export default LoginPage;
