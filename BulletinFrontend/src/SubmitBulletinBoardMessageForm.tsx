import { useAuth0 } from "@auth0/auth0-react";
import { useEffect, useState } from "react";
import { IBulletinBoardMessage } from "./BulletinBoard";

export interface ISubmit {
    onAddMessage: (message: IBulletinBoardMessage) => void;
}

const SubmitBulletinBoardMessageForm = ({ onAddMessage }: ISubmit) => {
    const { getAccessTokenSilently } = useAuth0();
    const [myAccessToken, setMyAccessToken] = useState("");
    const [message, setMessage] = useState("");
    const [isFetching, setIsFetching] = useState(false);

    useEffect(() => {
        const fetchToken = async () => {
            return await getAccessTokenSilently();
        };
        fetchToken().then(token => {
            setMyAccessToken(token);
        });
    }, [0]);

    const onHandleSubmit = (e: any) => {
        e.preventDefault();
        setIsFetching(true);
        const url = import.meta.env.VITE_API_URL + "/bulletinboard";
        fetch(url, {
            body: `message=${encodeURIComponent(message)}`,
            headers: {
                Authorization: `Bearer ${myAccessToken}`,
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            mode: 'cors',
            method: 'POST'
        }).then((r) => r.json())
            .then(json => {
                setMessage("");
                onAddMessage(json);
                setIsFetching(false);
            })
            .catch(e => {
                console.log("oh no, exception", e);
                setIsFetching(false);
            });
    };

    return (
        <form onSubmit={onHandleSubmit}>
            <label>
                Melding:
                <input type="text" value={message} onChange={(ev) => setMessage(ev.target.value)}></input>
            </label>
            <input type="submit" value="Send" disabled={isFetching} />
        </form>
    );
}

export default SubmitBulletinBoardMessageForm;