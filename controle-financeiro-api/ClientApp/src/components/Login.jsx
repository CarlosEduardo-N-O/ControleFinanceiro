// src/components/Login.jsx
import React, { useState } from "react";

const Login = ({ onLogin }) => {
    const [usuario, setUsuario] = useState("");
    const [senha, setSenha] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();

        // Chamada para o backend
        try {
            const response = await fetch("/api/Usuario/login", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ email, senha }),
            });

            if (response.ok) {
                const usuario = await response.json();
                onLogin(usuario);
            } else {
                alert("Usuario ou senha inválidos!");
            }
        } catch (error) {
            console.error("Erro ao fazer login:", error);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="login-form">
            <h2>Login</h2>
            <input
                type="Usuario"
                placeholder="Usuario"
                value={email}
                onChange={(e) => setUsuario(e.target.value)}
                required
            />
            <input
                type="password"
                placeholder="Senha"
                value={senha}
                onChange={(e) => setSenha(e.target.value)}
                required
            />
            <button type="submit">Entrar</button>
        </form>
    );
};

export default Login;
